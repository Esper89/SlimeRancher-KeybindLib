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

namespace KeybindLib
{
    public class Keybind
    {
        public Keybind(
            Binding[] defaultBindings,
            string name,
            string? comesBefore = null
        )
        {
            this.DefaultBindings = defaultBindings;
            this.Name = name;
            this.ComesBefore = comesBefore;

            this.ActionName = "Act." + this.Name;
        }

        public Binding[] DefaultBindings { get; }
        public string Name { get; }
        public string? ComesBefore { get; }

        protected internal string ActionName { get; }
    }
}
