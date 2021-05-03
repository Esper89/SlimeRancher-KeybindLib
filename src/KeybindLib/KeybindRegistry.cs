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
using InControl;
using System.Collections.Generic;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib
{
    public static class KeybindRegistry
    {
        public static void Register(ModKeybind keybind)
        {
            Reg.keybinds.Add(keybind);
            Reg.InsertBeforeAdd(keybind);
        }

        internal static readonly List<ModKeybind> keybinds
            = new List<ModKeybind> { };

        internal static readonly Dictionary<ModKeybind, PlayerAction> actions
            = new Dictionary<ModKeybind, PlayerAction> { };

        internal static readonly Dictionary<string, List<ModKeybind>> insertBefore
            = new Dictionary<string, List<ModKeybind>> { };

        private static void InsertBeforeAdd(ModKeybind keybind)
        {
            if (Reg.insertBefore.ContainsKey(keybind.KeybindName))
            {
                Reg.insertBefore[keybind.KeybindName]
                    .Add(keybind);
            }
            else
            {
                Reg.insertBefore[keybind.KeybindName]
                    = new List<ModKeybind> { keybind };
            }
        }
    }
}
