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
using System.Reflection.Emit;
using HarmonyLib;
using Instrs = System.Collections.Generic.IEnumerable<HarmonyLib.CodeInstruction>;

namespace KeybindLib.Patches
{
    [HarmonyPatch(typeof(vp_FPInput), nameof(vp_FPInput.Update))]
    internal static class UpdatePatch // Allows us to update keybinds at the right time.
    {
        internal static Instrs Transpiler(Instrs instructions)
        {
            // Method called even when the game is paused.
            MethodInfo methodPaused = AccessTools.Method(typeof(vp_FPInput), nameof(vp_FPInput.UpdateCursorLock));

            // Method called only when the game isn't paused.
            MethodInfo methodNonPaused = AccessTools.Method(typeof(vp_FPInput), nameof(vp_FPInput.InputInteract));

            #region explanation

            /* This transpiler returns each CodeInstruction after it's had a chance to decide what to do with it,
             * meaning we can insert our own method calls before Slime Rancher's calls. We do this twice; once for
             * the section of methods that will always run, even when the game is paused, and then again for the
             * methods that will only run when the game is paused.

               ...
               ldarg.0
             > ldarg.0
             > call Keybind.EventUpdatePaused(this)
             ! callvirt this.UpdateCursorLock()
               ...
               ldarg.0
             > ldarg.0
             > call Keybind.EventUpdate(this)
             ! callvirt this.InputInteract()
               ...

             * As you can see, the `callvirt` instruction that we're looking out for ends up coming after the code
             * that we insert into the method. This allows our code to run before any of the game's does.
             */

            #endregion

            foreach (var instr in instructions)
            {
                if (instr.opcode == OpCodes.Callvirt) // Can't cast operand to MethodInfo without checking.
                {
                    if ((MethodInfo)instr.operand == methodPaused) // Paused only.
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Call,
                            AccessTools.Method(typeof(Keybind), nameof(Keybind.EventUpdatePaused))
                        );
                    }

                    if ((MethodInfo)instr.operand == methodNonPaused) // Not paused.
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Call,
                            AccessTools.Method(typeof(Keybind), nameof(Keybind.EventUpdate))
                        );
                    }
                }

                yield return instr;
            }
        }
    }
}
