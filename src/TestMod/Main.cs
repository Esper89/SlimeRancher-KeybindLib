//  Copyright (c) 2021 Esper Thomson
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using InControl;
using KeybindLib;
using SRML;

namespace TestMod
{
    public class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
            KeybindRegistry.Register(
                new Keybind(
                    defaultBindings: new Binding[] { Key.T },
                    name: "key.testkey",
                    comesBefore: "key.reportissue"
                )
            );

            KeybindRegistry.Register(
                new Keybind(
                    defaultBindings: new Binding[] { Key.J },
                    name: "key.jestkey",
                    comesBefore: "key.reportissue"
                )
            );

            KeybindRegistry.Register(
                new Keybind(
                    defaultBindings: new Binding[] { Key.K },
                    name: "key.kestkey",
                    comesBefore: "key.screenshot"
                )
            );
        }
    }
}
