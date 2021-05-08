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

using InControl;
using KeybindLib;

namespace TestMod
{
    public static class Keybinds // A class for holding keybinds.
    {
        public static Keybind Test { get; } // A property for each keybind.
            = new Keybind(
                defaultBindings: new Binding[] { Key.T },
                name: "key.test",
                comesBefore: "key.reportissue"
            );

        public static Keybind Jest { get; }
            = new Keybind(
                defaultBindings: new Binding[] { Key.J }, // The keys that it should be bound to by default.
                name: "key.jest"
            ); // comesBefore is optional; omitting it will set it to null.

        public static Keybind Kest { get; }
            = new Keybind(
                defaultBindings: new Binding[] { Key.K },
                name: "key.kest", // The name of the keybind; should start with "key".
                comesBefore: "key.screenshot"
            );

        public static Keybind Vest { get; }
            = new Keybind(
                defaultBindings: new Binding[] { Key.V },
                name: "key.vest",
                comesBefore: "key.left" // The name of the key that this keybind should come before. Indicates this keybind's position in the list.
            );

        public static Keybind Nest { get; }
            = new Keybind(
                defaultBindings: new Binding[] { Key.N },
                name: "key.nest",
                comesBefore: Keybind.BEGINNING_OF_LIST // This can be `Keybind.BEGINNING_OF_LIST` for the beginning of the list, or you can leave it as null for the end of the list.
            );

        internal static void Register() // A method for registering all your keybinds. Call during PreLoad.
        {
            KeybindRegistry.Register(new[]
            {
                Keybinds.Test, // Each keybind to register.
                Keybinds.Jest,
                Keybinds.Kest,
                Keybinds.Vest,
                Keybinds.Nest
            });
        }
    }
}
