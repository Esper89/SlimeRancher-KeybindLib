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

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using IEnumCInstr = System.Collections.Generic.IEnumerable<HarmonyLib.CodeInstruction>;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib.Patches
{
    internal static class InputPatch // For patching the options menu and gamepad panel.
    {
        [HarmonyPatch(typeof(OptionsUI), nameof(OptionsUI.SetupInput))]
        internal static class KeyboardInputPatch // Options menu.
        {
            internal static IEnumCInstr Transpiler(IEnumCInstr instructions)
                => Transpile(instructions);
        }

        [HarmonyPatch(typeof(GamepadPanel), nameof(GamepadPanel.SetupBindings))]
        internal static class GamepadInputPatch // Gamepad panel.
        {
            internal static IEnumCInstr Transpiler(IEnumCInstr instructions)
                => Transpile(instructions);
        }

        private static IEnumCInstr Transpile(IEnumCInstr instructions) // Godspeed.
        {
            MethodInfo method = AccessTools.Method(typeof(InputPatch), nameof(CreateBindingLines));

            IEnumCInstr InsertCall(OpCode opcode, object? operand = null)
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                yield return new CodeInstruction(opcode, operand);
                yield return new CodeInstruction(OpCodes.Call, method);
            }

            #region explanation

            /* This transpiler acts in three ways, all of which just insert
             * calls to a specific method, with varying arguments. These
             * method calls look like this:

               ldarg.0
             ? ldstr <string> : ldnull
               call <method>(this, string)

             * First, they load argument zero (`this`) with ldarg.0. Then,
             * they load a string or null to tell the method which keybinds
             * need to be created at this time. Finally, they call the method,
             * popping those two arguments off the stack.

             * The first task is to find the first ldstr with an operand that
             * starts with "key." and inserts a call directly after that, to
             * load keybinds that have to go at the start of the list:

               ...
               ldarg.0
             ! ldstr key.<anything>
             > ldarg.0
             > ldstr <BEGINNING_OF_LIST>
             > call <method>(this, string)
               call SRInput.get_Actions()
               ldfld PlayerActions.<action>
               call this.CreateBindingLine(string, PlayerAction)
               pop
               ...

             * The second task is checking every subsequent ldstr call, and if
             * it matches a string in the ComesAfter dictionary, that string
             * is loaded and the method is called, to specify exactly which
             * keybinds to create at this time:

               ...
               ldarg.0
             ! ldstr <key>
             > ldarg.0
             > ldstr <key>
             > call <method>(this, string)
               call SRInput.get_Actions()
               ldfld PlayerActions.<action>
               call this.CreateBindingLine(string, PlayerAction)
               pop
               ...

             * The third task is to wait until ldc.i4.1, which indicates the
             * end of the list. Then, calls need to be inserted once for each
             * string that wasn't found while traversing the main list. After
             * all those are done, a call using null is performed, to create
             * all the keybinds that should be at the end of the list:

               ...
               ldarg.0
               ldfld this.bindingsPanel
             ! ldc.i4.1
             > ldarg.0
             > ldstr <unaccounted_key>
             > call<method>(this, string)
             > ...
             > ldarg.0
             > ldnull
             > call <method>(this, string)
               callvirt bindingsPanel.GetComponentsInChildren<Button>(bool)
               stloc.1
               ldc.i4.0
               stloc.3
               ...

             * And that's how this transpiler works. I figured the source code
             * was the most appropriate place to write an explanation, as it
             * is always where it needs to be and can be modified when
             * necessary. If you make changes to this transpiler, please also
             * update this explanation.
             */

            #endregion

            bool started = false;
            bool ended = false;
            var unaccounted = new HashSet<string>(Reg.comesBefore.Keys);

            foreach (CodeInstruction instr in instructions)
            {
                yield return instr;

                if (ended) continue;

                if (instr.opcode == OpCodes.Ldstr)
                {
                    if (!started && ((string)instr.operand).StartsWith(Keybind.KEYBIND_PREFIX)) // Insert BEGINNING_OF_LIST entries at the start of the list.
                    {
                        foreach (var ins in InsertCall(OpCodes.Ldstr, Keybind.BEGINNING_OF_LIST)) yield return ins;
                        unaccounted.Remove(Keybind.BEGINNING_OF_LIST);

                        started = true;
                    }

                    if (Reg.comesBefore.ContainsKey((string)instr.operand)) // Insert regular entries into the middle of the list.
                    {
                        foreach (var ins in InsertCall(OpCodes.Ldstr, instr.operand)) yield return ins;

                        unaccounted.Remove((string)instr.operand);
                    }
                }

                if (started && instr.opcode == OpCodes.Ldc_I4_1) // Insert unaccounted for and null entries at the end of the list.
                {
                    foreach (string key in unaccounted)
                    {
                        foreach (var ins in InsertCall(OpCodes.Ldstr, key)) yield return ins;
                    }

                    foreach (var ins in InsertCall(OpCodes.Ldnull)) yield return ins;

                    ended = true;
                }
            }

            // If the methods this thing patches change, even just a bit, it'll probably stop working. Here there be dragons.
        }

        private static void CreateBindingLines(object instance, string? keyName)
        {
            List<Keybind> keybinds;

            if (keyName is null) // End of the list.
            {
                keybinds = Reg.endOfListKeybinds;
            }
            else if (Reg.comesBefore.ContainsKey(keyName)) // Anywhere else in the list.
            {
                keybinds = Reg.comesBefore[keyName];
            }
            else
            {
                throw new System.ArgumentException(nameof(keyName)); // If this happens I will be sad.
            }

            foreach (Keybind keybind in keybinds)
            {
                if (instance is OptionsUI)
                {
                    keybind.RegisterTranslations();

                    ((OptionsUI)instance).CreateKeyBindingLine(keybind.Name, keybind.Action);
                }
                else if (instance is GamepadPanel)
                {
                    // Translations are registered only once.

                    ((GamepadPanel)instance).CreateGamepadBindingLine(keybind.Name, keybind.Action);
                }
                else
                {
                    throw new System.ArgumentException(nameof(instance)); // How? Why?
                }
            }
        }
    }
}
