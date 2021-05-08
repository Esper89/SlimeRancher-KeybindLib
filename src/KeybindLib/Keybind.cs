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

namespace KeybindLib
{
    /// <summary> A keybind. </summary>
    /// <seealso cref="KeybindRegistry"/>
    public class Keybind
    {
        /// <summary> Creates a new <see cref="Keybind"/>. </summary>
        /// <param name="defaultBindings"> This instance's <see cref="DefaultBindings"/>. </param>
        /// <param name="name"> This instance's <see cref="Name"/>. </param>
        /// <param name="comesBefore"> This instance's <see cref="ComesBefore"/>. </param>
        public Keybind(
            Binding[] defaultBindings,
            string name,
            string? comesBefore = null
        )
        {
            this.DefaultBindings = defaultBindings;
            this.Name = name;
            this.ComesBefore = comesBefore;
        }

        /// <summary> The default <see cref="Binding"/>s for this instance. </summary>
        public virtual Binding[] DefaultBindings { get; }

        /// <summary> This instance's name. </summary>
        /// <remarks> Should be in the format "key.my_key". </remarks>
        public virtual string Name { get; }

        /// <summary> The name of the vanilla keybind that should come after this instance. </summary>
        /// <remarks> Indicates where in the list of keybinds this one should go. </remarks>
        /// <seealso cref="MethodKeybindExtractor.VanillaKeybinds"/>
        /// <seealso cref="BEGINNING_OF_LIST"/>
        public virtual string? ComesBefore { get; }

        /// <summary> The name of this keybind's corresponding <see cref="PlayerAction"/>. </summary>
        protected internal virtual string ActionName => $"Action:{this.Name}";

        /// <summary> A special string that can be used as <see cref="ComesBefore"/> to represent the beginning of the list. </summary>
        public const string BEGINNING_OF_LIST = "BEGINNING";

        /// <summary> The expected prefix that every keybind's name should have. </summary>
        public const string KEYBIND_PREFIX = "key.";

        /// <summary> This instance's <seealso cref="PlayerAction"/>. </summary>
        /// <exception cref="KeybindNotReadyException"> Thrown when this is accessed before the Load step. </exception>
        public PlayerAction Action
        {
            get => this._action ?? throw new KeybindNotReadyException(this);

            internal set
            {
                this._action = value;
                this.BindAllDefaultsTo(value);
            }
        }
        private PlayerAction? _action;

        /// <summary> Binds all <see cref="DefaultBindings"/> of this instance to the given <see cref="PlayerAction"/>. </summary>
        /// <param name="action"> The <see cref="PlayerAction"/> to bind to. </param>
        protected virtual void BindAllDefaultsTo(PlayerAction action)
        {
            foreach (Binding binding in this.DefaultBindings)
            {
                binding.BindTo(action);
            }
        }

        /// <summary> An exception thrown when <see cref="Action"/> is accessed before it is ready. </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public class KeybindNotReadyException : InvalidOperationException
        {
            /// <summary> Creates a new <see cref="KeybindNotReadyException"/>. </summary>
            /// <param name="keybind"> This instance's <see cref="Keybind"/>. </param>
            protected internal KeybindNotReadyException(Keybind keybind) : base(
                $"This {nameof(KeybindLib.Keybind)} ({keybind.Name}) has not been fully set up yet. " +
                $"Please wait until {nameof(SRInput.PlayerActions)} has been initialized before accessing " +
                $"{nameof(KeybindLib.Keybind)}.{nameof(Action)}. " +
                $"{nameof(SRInput.PlayerActions)} is initialized between the Load and PreLoad steps."
            )
            {
                this.Keybind = keybind;
            }

            /// <summary> The <see cref="KeybindLib.Keybind"/> whose <see cref="Action"/> was accessed before it was ready. </summary>
            public Keybind Keybind { get; }
        }
    }
}
