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
using Button = InControl.InputControlType;

namespace KeybindLib
{
    /// <summary> A binding. </summary>
    /// <remarks> Represents the various types of bindings accepted by <see cref="InControl"/>. </remarks>
    public abstract class Bind
    {
        /// <summary> Adds this instance as a default binding for the specified <see cref="PlayerAction"/>. </summary>
        protected internal abstract void BindDefault(PlayerAction action);

        #region subclasses

        /// <summary> A <see cref="Key"/> <see cref="Bind"/>. </summary>
        public class KeyBind : Bind
        {
            /// <summary> Creates a new <see cref="Bind"/> from a <see cref="Key"/>. </summary>
            public KeyBind(Key key)
            {
                this.key = key;
            }

            private readonly Key key;

            protected internal override void BindDefault(PlayerAction action)
            {
                action.AddDefaultBinding(this.key);
            }
        }

        /// <summary> A <see cref="Button"/> <see cref="Bind"/>. </summary>
        public class ButtonBind : Bind
        {
            /// <summary> Creates a new <see cref="Bind"/> from a <see cref="Button"/>. </summary>
            public ButtonBind(Button button)
            {
                this.button = button;
            }

            private readonly Button button;

            protected internal override void BindDefault(PlayerAction action)
            {
                action.AddDefaultBinding(this.button);
            }
        }

        /// <summary> A <see cref="Mouse"/> <see cref="Bind"/>. </summary>
        public class MouseBind : Bind
        {
            /// <summary> Creates a new <see cref="Bind"/> from a <see cref="Mouse"/>. </summary>
            public MouseBind(Mouse mouse)
            {
                this.mouse = mouse;
            }

            private readonly Mouse mouse;

            protected internal override void BindDefault(PlayerAction action)
            {
                action.AddDefaultBinding(this.mouse);
            }
        }

        #endregion

        #region operators

        /// <summary> Converts a <see cref="Key"/> to a <see cref="Bind"/>. </summary>
        public static implicit operator Bind(Key key)
            => new KeyBind(key);

        /// <summary> Converts a <see cref="Button"/> to a <see cref="Bind"/>. </summary>
        public static implicit operator Bind(Button button)
            => new ButtonBind(button);

        /// <summary> Converts a <see cref="Mouse"/> to a <see cref="Bind"/>. </summary>
        public static implicit operator Bind(Mouse mouse)
            => new MouseBind(mouse);

        #endregion
    }
}
