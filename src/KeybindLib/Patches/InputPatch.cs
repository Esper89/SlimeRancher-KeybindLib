//  Copyright (c) 2021 Esper Thomson
//
//  This file is part of KeybindLib.
//
//  KeybindLib is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  KeybindLib is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with KeybindLib.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using GP = KeybindLib.Patches.InputPatch.GamepadInputPatch;
using InstrEnum = System.Collections.Generic.IEnumerable<HarmonyLib.CodeInstruction>;
using KB = KeybindLib.Patches.InputPatch.KeyboardInputPatch;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib.Patches
{
    internal static class InputPatch
    {
        internal static void Apply() // Must be run after all mods are done preloading.
        {
            Type[] tpiler = new[] { typeof(InstrEnum) };

            Main.HarmonyInstance?.Patch(
                original: KB.targetMethod,
                transpiler: new HarmonyMethod(
                    AccessTools.Method(typeof(KB), nameof(KB.Transpiler), tpiler)
                )
            );

            Main.HarmonyInstance?.Patch(
                original: GP.targetMethod,
                transpiler: new HarmonyMethod(
                    AccessTools.Method(typeof(GP), nameof(GP.Transpiler), tpiler)
                )
            );
        }

        internal static class KeyboardInputPatch
        {
            internal static readonly MethodInfo targetMethod = AccessTools.Method(
                typeof(OptionsUI),
                nameof(OptionsUI.SetupInput),
                new Type[] { }
            );

            internal static InstrEnum Transpiler(InstrEnum instructions)
            {
                return Transpile(instructions, AccessTools.Method(
                    typeof(KB),
                    nameof(KB.CreateKeyBindingLines),
                    new[] { typeof(OptionsUI), typeof(string) }
                ));
            }

            private static void CreateKeyBindingLines(OptionsUI instance, string keyName)
            {
                foreach (Keybind keybind in Reg.insertBefore[keyName])
                {
                    instance.CreateKeyBindingLine(
                        keybind.Name,
                        Reg.actions[keybind]
                    );
                }
            }
        }

        internal static class GamepadInputPatch
        {
            internal static readonly MethodInfo targetMethod = AccessTools.Method(
                typeof(GamepadPanel),
                nameof(GamepadPanel.SetupBindings),
                new Type[] { }
            );

            internal static InstrEnum Transpiler(InstrEnum instructions)
                => Transpile(instructions, AccessTools.Method(
                    typeof(GP),
                    nameof(GP.CreateGamepadBindingLines),
                    new[] { typeof(GamepadPanel), typeof(string) }
                ));

            private static void CreateGamepadBindingLines(GamepadPanel instance, string keyName)
            {
                foreach (Keybind keybind in Reg.insertBefore[keyName])
                {
                    instance.CreateGamepadBindingLine(
                        keybind.Name,
                        Reg.actions[keybind]
                    );
                }
            }
        }

        private static InstrEnum Transpile(
            InstrEnum instructions,
            MethodInfo method // void (object, string)
        )
        {
            /* ...
               ldarg.0
               ldstr <key>
             > ldarg.0
             > ldstr <key>
             > call <method>(object, string)
               call SRInput.get_Actions()
               ldfld PlayerActions.<action>
               call this.CreateKeyBinding(string, PlayerAction)
               pop
               ...
             */

            // TODO Ensure null entries still get added to the list.
            // Preferably at the bottom of the list.

            bool started = false;
            bool done = false;
            // (Regarding the CreateKeyBinding calls)

            foreach (CodeInstruction instr in instructions)
            {
                yield return instr;

                if (!done &&
                    instr.opcode == OpCodes.Ldstr && // Ldstr indicates a new CreateKeyBinding call.
                    Reg.insertBefore.ContainsKey((string)instr.operand) // Operand is the name of the keybinding being created.
                )
                {
                    started = true; // First valid ldstr, now capable of "being done".

                    yield return new CodeInstruction(OpCodes.Ldarg_0); // ldarg.0
                    yield return new CodeInstruction(OpCodes.Ldstr, instr.operand); // ldstr <key>
                    yield return new CodeInstruction(OpCodes.Call, method); // call <method>(object, string)
                }
                else if (instr.opcode == OpCodes.Callvirt && started)
                {
                    done = true; // Callvirt indicates end of CreateKeyBinding calls.
                }
            }
        }
    }
}
