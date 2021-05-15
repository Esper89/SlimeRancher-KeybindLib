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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace KeybindLib
{
    /// <summary> Parses vanilla methods to extract keybind information. </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class MethodKeybindExtractor
    {
        static MethodKeybindExtractor() { } // beforefieldinit

        /// <summary> The set of all valid vanilla keybind names. </summary>
        /// <remarks> Mutating this object has no effect. Includes <seealso cref="Keybind.BEGINNING_OF_LIST"/>. </remarks>
        public static HashSet<string> VanillaKeybinds
            => new HashSet<string>(MethodKeybindExtractor._keybinds); // Duplicate the set to prevent tampering.

        // Typical vanilla keybind list:

        /* key.forward
         * key.left
         * key.back
         * key.right
         * key.shoot
         * key.vac
         * key.burst
         * key.jump
         * key.run
         * key.interact
         * key.gadgetMode
         * key.flashlight
         * key.radar
         * key.map
         * key.slot_1
         * key.slot_2
         * key.slot_3
         * key.slot_4
         * key.slot_5
         * key.prev_slot
         * key.next_slot
         * key.reportissue
         * key.screenshot
         * key.recordgif
         * key.pedia
         */

        private static readonly HashSet<string> _keybinds
            = MethodKeybindExtractor.DumpAllKeybinds(); // Makes sure that this isn't run more than once.

        private static HashSet<string> DumpAllKeybinds()
        {
            HashSet<string> keyboardKeys = DumpKeybinds(
                method: AccessTools.Method(
                    typeof(OptionsUI),
                    nameof(OptionsUI.SetupInput)
                )
            );

            HashSet<string> gamepadKeys = DumpKeybinds(
                method: AccessTools.Method(
                    typeof(GamepadPanel),
                    nameof(GamepadPanel.SetupBindings)
                )
            );

            return new HashSet<string>(keyboardKeys.Union(gamepadKeys))
            {
                Keybind.BEGINNING_OF_LIST
            };
        }

        private static HashSet<string> DumpKeybinds(MethodInfo method)
        {
            var instructions = new List<CodeInstruction> { };

            MethodBodyReader.GetInstructions(GetILGenerator(), method).ForEach(
                (instr) => instructions.Add(instr.GetCodeInstruction())
            ); // Uses Harmony's internals, because the proper method is not present in SRML's version of Harmony.

            var set = new HashSet<string> { };

            foreach (CodeInstruction instr in instructions)
            {
                if (instr.opcode == OpCodes.Ldstr &&
                    ((string)instr.operand).StartsWith(Keybind.KEYBIND_PREFIX))
                {
                    set.Add((string)instr.operand);
                }
            }

            return set;

            static ILGenerator GetILGenerator()
            {
                Func<string> name = Guid.NewGuid().ToString; // Mwahahahaha!

                return AppDomain.CurrentDomain
                    .DefineDynamicAssembly(new AssemblyName(name()), AssemblyBuilderAccess.RunAndSave)
                    .DefineDynamicModule(name())
                    .DefineType(name())
                    .DefineMethod(name(), MethodAttributes.Public)
                    .GetILGenerator();

                // Who do I need to create a whole-ass assembly just to get an ILGenerator?
            }
        }
    }
}
