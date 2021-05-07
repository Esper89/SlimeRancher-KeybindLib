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

using HarmonyLib;
using InControl;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib.Patches
{
    [HarmonyPatch(typeof(SRInput.PlayerActions), MethodType.Constructor, new[] { typeof(SRInput) })]
    internal static class PlayerActionsPatch // Creates the actions that correspond to each keybind. Runs between PreLoad and Load.
    {
        internal static void Postfix(SRInput.PlayerActions __instance)
        {
            foreach (Keybind keybind in Reg.keybinds)
            {
                PlayerAction action = __instance.CreatePlayerAction(keybind.ActionName);
                keybind.Action = action;
            }
        }
    }
}
