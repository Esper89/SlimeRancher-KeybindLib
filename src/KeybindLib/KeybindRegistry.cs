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
using System.Collections.Generic;
using System.ComponentModel;
using Reg = KeybindLib.KeybindRegistry;

namespace KeybindLib
{
    /// <summary> Registers <see cref="Keybind"/>s and keeps track of metadata. </summary>
    public static class KeybindRegistry
    {
        /// <summary> Registers a <see cref="Keybind"/>. </summary>
        /// <param name="keybind"> The <see cref="Keybind"/> to register.  </param>
        /// <exception cref="KeybindInvalidException"> Thrown when <paramref name="keybind"/> has an invalid <see cref="Keybind.Name"/> or <see cref="Keybind.ComesBefore"/>. </exception>
        /// <exception cref="KeybindRegisteredTooLateException"> Thrown when this method is called after PreLoad. </exception>
        /// <remarks> Registered keybinds must have a unique <see cref="Keybind.Name"/> and a valid <see cref="Keybind.ComesBefore"/>. </remarks>
        /// <seealso cref="Register(IEnumerable{Keybind})"/>
        public static void Register(Keybind keybind)
        {
            Reg.ValidateKeybind(keybind);

            Reg.registeredNames.Add(keybind.Name);
            Reg.keybinds.Add(keybind);
            Reg.AddKeybindPosition(keybind);
        }

        /// <summary> Registers a collection of <see cref="Keybind"/>s. </summary>
        /// <param name="keybinds"> The <see cref="Keybind"/>s to register. </param>
        /// <seealso cref="Register(Keybind)"/>
        /// <inheritdoc cref="Register(Keybind)"/>
        public static void Register(IEnumerable<Keybind> keybinds)
        {
            foreach (Keybind keybind in keybinds)
            {
                Reg.Register(keybind);
            }
        }

        #region collections

        private static readonly HashSet<string> registeredNames
            = new HashSet<string> { };

        internal static readonly List<Keybind> keybinds
            = new List<Keybind> { };

        internal static readonly Dictionary<string, List<Keybind>> comesBefore
            = new Dictionary<string, List<Keybind>>
            {
                [Keybind.BEGINNING_OF_LIST] = new List<Keybind> { }
            };

        internal static readonly List<Keybind> endOfListKeybinds
            = new List<Keybind> { };

        #endregion

        private static void AddKeybindPosition(Keybind keybind)
        {
            if (keybind.ComesBefore is null)
            {
                Reg.endOfListKeybinds.Add(keybind);
            }
            else
            {
                if (Reg.comesBefore.ContainsKey(keybind.ComesBefore))
                {
                    Reg.comesBefore[keybind.ComesBefore].Add(keybind);
                }
                else
                {
                    Reg.comesBefore[keybind.ComesBefore] = new List<Keybind> { keybind };
                }
            }
        }

        #region validation

        private static void ValidateKeybind(Keybind keybind)
        {
            if (Main.hasPreloaded)
            {
                throw new KeybindRegisteredTooLateException(keybind);
            }
            else if (Reg.registeredNames.Contains(keybind.Name) ||
                MethodKeybindExtractor.VanillaKeybinds.Contains(keybind.Name))
            {
                throw new KeybindInvalidException(keybind, KeybindInvalidException.Reason.NameTaken);
            }
            else if (keybind.ComesBefore is object &&
                !MethodKeybindExtractor.VanillaKeybinds.Contains(keybind.ComesBefore))
            {
                throw new KeybindInvalidException(keybind, KeybindInvalidException.Reason.ComesBeforeMissing);
            }
        }

        /// <summary> An exception thrown when an invalid <see cref="Keybind"/> is registered. </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public sealed class KeybindInvalidException : ArgumentException
        {
            internal enum Reason
            {
                NameMissingPrefix, // When the keybind name is missing it's expected prefix.
                NameTaken, // When the keybind name is already taken.
                ComesBeforeMissing // When the keybind ComesBefore isn't in the list of vanilla keybinds.
            }

            internal KeybindInvalidException(Keybind keybind, Reason reason) : base(
                $@"Attempted to register keybind {reason switch
                {
                    Reason.NameMissingPrefix => $" with an invalid name ({keybind.Name}). Keybind names must start with \"{ Keybind.KEYBIND_PREFIX }\".",
                    Reason.NameTaken => $"with existing name ({keybind.Name}). Cannot register duplicate keybind.",
                    Reason.ComesBeforeMissing => $"at a nonexistant point in the keybind list ({keybind.ComesBefore}).",
                    _ => throw new ArgumentOutOfRangeException(nameof(reason))
                }}"
            )
            {
                this.Keybind = keybind;
            }

            /// <summary> The invalid <see cref="Keybind"/> in question. </summary>
            public Keybind Keybind { get; }
        }

        /// <summary> An exception thrown when a <see cref="KeybindLib.Keybind"/> is registered after <see cref="KeybindLib"/> has been PreLoaded. </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public sealed class KeybindRegisteredTooLateException : InvalidOperationException
        {
            internal KeybindRegisteredTooLateException(Keybind keybind) : base(
                $"Attempted to register {nameof(KeybindLib.Keybind)} after {nameof(Main.PreLoad)}. " +
                $"Make sure {nameof(KeybindLib)} is in your mod's 'load_before' in your modinfo.json"
            )
            {
                this.Keybind = keybind;
            }

            /// <summary> The <see cref="Keybind"/> that was registered too late. </summary>
            public Keybind Keybind { get; }
        }

        #endregion
    }
}
