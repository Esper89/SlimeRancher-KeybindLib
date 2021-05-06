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
using Button = InControl.InputControlType;

namespace KeybindLib
{
    public struct Binding
    {
        public Binding(Key key)
        {
            this.Key = key;
        }

        public Binding(Button button)
        {
            this.Key = button;
        }

        public Binding(Mouse mouse)
        {
            this.Key = mouse;
        }

        internal object Key { get; }

        #region operators

        public static implicit operator Binding(Key key)
            => new Binding(key);

        public static implicit operator Binding(Button button)
            => new Binding(button);

        public static implicit operator Binding(Mouse mouse)
            => new Binding(mouse);

        #endregion

        internal void BindTo(PlayerAction action)
        {
            Type type = this.Key.GetType();

            if (type.Equals(typeof(Key)))
            {
                action.AddDefaultBinding((Key)this.Key);
            }
            else if (type.Equals(typeof(Button)))
            {
                action.AddDefaultBinding((Button)this.Key);
            }
            else if (type.Equals(typeof(Mouse)))
            {
                action.AddDefaultBinding((Mouse)this.Key);
            }
            else // Good luck.
            {
                throw new InvalidOperationException($"{nameof(Binding)} instance is not a valid type. " +
                    $"Valid types are: {typeof(Key).FullName}, {typeof(Button).FullName}, {typeof(Mouse).FullName}");
            }
        }
    }
}
