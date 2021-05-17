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
    internal static class UpdatePatch // Allows us to update keybinds at the right time every frame.
    {
        internal static Instrs Transpiler(Instrs instructions)
        {
            MethodInfo targetMethod = AccessTools.Method(typeof(vp_FPInput), nameof(vp_FPInput.InputCamera));

            #region explanation

            /* This transpiler returns each CodeInstruction and then checks if it's the one we're looking for. If it
             * is, we insert some instructions to call `KeybindRegistry.UpdateAll` to update all the keybinds at the
             * right time, after all the vanilla keybinds.

               ...
               ldarg.0
             ! callvirt this.InputCamera()
             > ldarg.0
             > call KeybindRegistry.UpdateAll(this)
               ret

             * We want this instruction to be right at the end of the method, in case modders want to override
             * vanilla behaviour. Since most of the vanilla methods set values that are picked up by other methods
             * later, putting our method call after theirs is perfect.
             */

            #endregion

            foreach (var instr in instructions)
            {
                yield return instr;

                if (instr.opcode == OpCodes.Callvirt && (MethodInfo)instr.operand == targetMethod) // callvirt this.InputInteract()
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0); // ldarg.0

                    yield return new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(KeybindRegistry), nameof(KeybindRegistry.UpdateAll))
                    ); // call KeybindRegistry.UpdateAll(this)
                }
            }
        }
    }
}
