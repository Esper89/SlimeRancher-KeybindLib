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
using System.Reflection.Emit;
using HarmonyLib;
using InControl;
using LList = System.Collections.Generic.LinkedList<HarmonyLib.CodeInstruction>;
using Node = System.Collections.Generic.LinkedListNode<HarmonyLib.CodeInstruction>;
using Reg = KeybindLib.KeybindRegistry;


namespace KeybindLib.Patches
{
    [HarmonyPatch(typeof(InputDirector), nameof(InputDirector.InitializeDefaultBindings))]
    internal static class DefaultBindingsPatch
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            LList instrs = new LList(instructions);

            Node? newarr = null;
            Node? lastStelem = null;

            #region find instructions

            Node? head = instrs.First;
            for (int i = 0; i < instrs.Count; i++)
            {
                OpCode op = head.Value.opcode;

                if (op == OpCodes.Newarr)
                {
                    newarr ??= head;
                }
                else if (op == OpCodes.Stelem_Ref)
                {
                    lastStelem = head;
                }
                else if (op == OpCodes.Stsfld)
                {
                    break;
                }

                head = head.Next;
            }

            if (newarr is null || lastStelem is null)
            {
                throw new Exception(
                    $"Could not find transpiler instruction for {nameof(DefaultBindingsPatch)}!"
                );
            }

            #endregion

            int oldArrayLength = (sbyte)newarr.Previous.Value.operand;

            // Replace array length.
            instrs.Remove(newarr.Previous);
            instrs.AddBefore(newarr, new CodeInstruction(
                OpCodes.Ldc_I4_S, oldArrayLength + Reg.keybinds.Count
            ));

            // Add extra elements.
            for (int i = 0; i < Reg.keybinds.Count; i++)
            {
                head = lastStelem;
                foreach (CodeInstruction instr in AddArrayElement(i, oldArrayLength))
                {
                    instrs.AddAfter(head, instr);
                    head = head.Next;
                }
            }

            return instrs;
        }

        internal static IEnumerable<CodeInstruction> AddArrayElement(int index, int oldArrayLength)
        {
            yield return new CodeInstruction(OpCodes.Dup); // Duplicate the array reference.

            yield return new CodeInstruction(OpCodes.Ldc_I4_S, oldArrayLength + index); // Push the array index.

            yield return new CodeInstruction(OpCodes.Ldc_I4_S, index); // Push this binding's index.

            yield return new CodeInstruction(OpCodes.Call, AccessTools.Method( // Call CreateDefaultBinding.
                typeof(DefaultBindingsPatch),
                nameof(DefaultBindingsPatch.GetDefaultBinding),
                new Type[] { typeof(int) }
            ));

            yield return new CodeInstruction(OpCodes.Stelem_Ref); // Push the newly-created default binding.
        }

        internal static InputDirector.DefaultBinding GetDefaultBinding(int index)
        {
            return Reg.keybinds[index].CreateBinding();
        }
    }
}
