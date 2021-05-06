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
using InControl;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib
{
    public static class KeybindRegistry
    {
        public static void Register(Keybind keybind)
        {
            Reg.keybinds.Add(keybind);
            Reg.AddToInsertBefore(keybind);
        }

        internal static readonly List<Keybind> keybinds
            = new List<Keybind> { };

        internal static readonly Dictionary<Keybind, PlayerAction> actions // Must be manually bound to each keybind.
            = new Dictionary<Keybind, PlayerAction> { };

        internal static readonly Dictionary<string?, List<Keybind>> insertBefore // Necessary to load a keybind.
            = new Dictionary<string?, List<Keybind>> { };

        private static void AddToInsertBefore(Keybind keybind)
        {
            if (Reg.insertBefore.ContainsKey(keybind.ComesBefore))
            {
                Reg.insertBefore[keybind.ComesBefore]
                    .Add(keybind);
            }
            else
            {
                Reg.insertBefore[keybind.ComesBefore]
                    = new List<Keybind> { keybind };
            }
        }
    }
}
