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

using System.Reflection;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InstrEnum = System.Collections.Generic.IEnumerable<HarmonyLib.CodeInstruction>;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib.Patches
{
    internal static class InputPatch // For patching the options menu and gamepad panel.
    {
        [HarmonyPatch(typeof(OptionsUI), nameof(OptionsUI.SetupInput))]
        internal static class KeyboardInputPatch // Options menu.
        {
            internal static InstrEnum Transpiler(InstrEnum instructions)
            {
                return Transpile(instructions, AccessTools.Method(
                    typeof(KeyboardInputPatch),
                    nameof(KeyboardInputPatch.CreateKeyBindingLines),
                    new[] { typeof(OptionsUI), typeof(string) }
                ));
            }

            private static void CreateKeyBindingLines(OptionsUI instance, string keyName)
            {
                foreach (Keybind keybind in Reg.comesBefore[keyName])
                {
                    instance.CreateKeyBindingLine(
                        keybind.Name,
                        keybind.Action
                    );
                }
            }
        }

        [HarmonyPatch(typeof(GamepadPanel), nameof(GamepadPanel.SetupBindings))]
        internal static class GamepadInputPatch // Gamepad panel.
        {
            internal static InstrEnum Transpiler(InstrEnum instructions)
                => Transpile(instructions, AccessTools.Method(
                    typeof(GamepadInputPatch),
                    nameof(GamepadInputPatch.CreateGamepadBindingLines),
                    new[] { typeof(GamepadPanel), typeof(string) }
                ));

            private static void CreateGamepadBindingLines(GamepadPanel instance, string? keyName)
            {
                List<Keybind> list;

                if (keyName is null)
                {
                    list = Reg.defaultKeybinds;
                }
                else
                {
                    list = Reg.comesBefore[keyName];
                }

                foreach (Keybind keybind in list)
                {
                    instance.CreateGamepadBindingLine(
                        keybind.Name,
                        keybind.Action
                    );
                }
            }
        }

        private static InstrEnum Transpile(InstrEnum instructions, MethodInfo method /* void (object, string) */)
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

            foreach (CodeInstruction instr in instructions)
            {
                yield return instr;

                if (instr.opcode == OpCodes.Ldstr && Reg.comesBefore.ContainsKey((string)instr.operand))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0); // ldarg.0
                    yield return new CodeInstruction(OpCodes.Ldstr, instr.operand); // ldstr <key>
                    yield return new CodeInstruction(OpCodes.Call, method); // call <method>(object, string)
                }
            }
        }
    }
}
