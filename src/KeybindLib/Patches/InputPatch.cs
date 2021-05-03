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
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib.Patches
{
    internal static class InputPatch
    {
        [HarmonyPatch(typeof(OptionsUI), nameof(OptionsUI.SetupInput))]
        internal static class KeyboardInputPatch
        {
            internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                return Transpile(instructions, AccessTools.Method(
                    typeof(KeyboardInputPatch),
                    nameof(KeyboardInputPatch.CreateKeyBindingLines),
                    new Type[] { typeof(OptionsUI), typeof(string) }
                ));
            }

            internal static void CreateKeyBindingLines(OptionsUI instance, string keyName)
            {
                foreach (ModKeybind keybind in Reg.insertBefore[keyName])
                {
                    instance.CreateKeyBindingLine(
                        keybind.KeybindName,
                        Reg.actions[keybind]
                    );
                }
            }
        }

        [HarmonyPatch(typeof(GamepadPanel), nameof(GamepadPanel.SetupBindings))]
        internal static class GamepadInputPatch
        {
            internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                return Transpile(instructions, AccessTools.Method(
                    typeof(GamepadInputPatch),
                    nameof(GamepadInputPatch.CreateGamepadBindingLines),
                    new Type[] { typeof(GamepadPanel), typeof(string) }
                ));
            }

            internal static void CreateGamepadBindingLines(GamepadPanel instance, string keyName)
            {
                foreach (ModKeybind keybind in Reg.insertBefore[keyName])
                {
                    instance.CreateGamepadBindingLine(
                        keybind.KeybindName,
                        Reg.actions[keybind]
                    );
                }
            }
        }

        private static IEnumerable<CodeInstruction> Transpile(
            IEnumerable<CodeInstruction> instructions,
            MethodInfo method // void (object, string) // FIXME use generics and delegates for type safety
        )
        {
            bool started = false;
            bool done = false;
            // (With regards to the CreateKeyBinding calls)

            foreach (CodeInstruction instr in instructions)
            {
                yield return instr;

                if (!done &&
                    instr.opcode == OpCodes.Ldstr &&
                    Reg.insertBefore.ContainsKey((string)instr.operand)
                )
                {
                    started = true; // First valid ldstr.

                    /* ...
                       ldarg.0
                       ldstr <key>
                     > call <method>
                     > ldarg.0
                     > ldstr <key>
                       call SRInput.get_Actions()
                       ldfld PlayerActions.<action>
                       call this.CreateKeyBinding(string, PlayerAction)
                       pop
                       ...
                     */

                    yield return new CodeInstruction(OpCodes.Call, method);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldstr, instr.operand);
                }
                else if (instr.opcode == OpCodes.Callvirt && started)
                {
                    done = true; // Callvirt indicates end of CreateKeyBinding calls.
                }
            }
        }






    }
}
