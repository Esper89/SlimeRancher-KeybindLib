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
using HarmonyLib;
using SRML.Editor;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib.Patches
{
    // ReplacerCache.ClearCache is called right after all other mods finish preloading, and it's not on the stack during preload.
    [HarmonyPatch(typeof(ReplacerCache), nameof(ReplacerCache.ClearCache))]
    internal static class SRMLPreloadPatch
    {
        internal static void Postfix()
        {
#if DEBUG
            foreach (Keybind keybind in Reg.keybinds)
            {
                Log.Write($"{nameof(Keybind)} {keybind.Name} {nameof(keybind.ComesBefore)} {keybind.ComesBefore}.");
            }
#endif
            InputPatch.Apply();
        }
    }
}
