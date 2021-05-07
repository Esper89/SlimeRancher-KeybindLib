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
using System.ComponentModel;
using InControl;
using Button = InControl.InputControlType;

namespace KeybindLib
{
    /// <summary> A binding. </summary>
    /// <remarks> Represents the various types of bindings accepted by <see cref="InControl"/>. </remarks>
    public struct Binding
    {
        #region constructors

        /// <summary> Creates a new <see cref="Binding"/> from a <see cref="Key"/>. </summary>
        public Binding(Key key)
        {
            this.binding = key;
        }

        /// <summary> Creates a new <see cref="Binding"/> from a <see cref="Button"/>. </summary>
        public Binding(Button button)
        {
            this.binding = button;
        }

        /// <summary> Creates a new <see cref="Binding"/> from a <see cref="Mouse"/>. </summary>
        public Binding(Mouse mouse)
        {
            this.binding = mouse;
        }

        #endregion

        private readonly object binding;

        #region operators

        /// <summary> Converts a <see cref="Key"/> to a <see cref="Binding"/>. </summary>
        public static implicit operator Binding(Key key)
            => new Binding(key);

        /// <summary> Converts a <see cref="Button"/> to a <see cref="Binding"/>. </summary>
        public static implicit operator Binding(Button button)
            => new Binding(button);

        /// <summary> Converts a <see cref="Mouse"/> to a <see cref="Binding"/>. </summary>
        public static implicit operator Binding(Mouse mouse)
            => new Binding(mouse);

        #endregion

        internal void BindTo(PlayerAction action) // If you know how to do this better, please send a PR.
        {
            Type type = this.binding.GetType();

            if (type.Equals(typeof(Key)))
            {
                action.AddDefaultBinding((Key)this.binding);
            }
            else if (type.Equals(typeof(Button)))
            {
                action.AddDefaultBinding((Button)this.binding);
            }
            else if (type.Equals(typeof(Mouse)))
            {
                action.AddDefaultBinding((Mouse)this.binding);
            }
            else // Good luck.
            {
#if DEBUG
                Log.Write(new BindingNotValidException(this));
#else
                throw new BindingNotValidException(this);
#endif
            }
        }

        /// <summary> An exception thrown when a <see cref="KeybindLib.Binding"/> is in a heinously invalid state. </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public sealed class BindingNotValidException : InvalidCastException
        {
            internal BindingNotValidException(Binding binding) : base(
                $"{nameof(Binding)} instance is not a valid type. " +
                $"Valid types are: {typeof(Key).FullName}, {typeof(Button).FullName}, {typeof(Mouse).FullName}"
            )
            {
                this.Binding = binding;
            }

            /// <summary> The <see cref="KeybindLib.Binding"/> that was in an invalid state. </summary>
            public Binding Binding { get; }
        }
    }
}
