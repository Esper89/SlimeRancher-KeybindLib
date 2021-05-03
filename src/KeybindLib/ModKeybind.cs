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
using Btn = InControl.InputControlType;
using Reg = KeybindLib.KeybindRegistry;
using VDefaultBinding = InputDirector.DefaultBinding;

namespace KeybindLib
{
    public sealed class ModKeybind
    {
        public ModKeybind(
            DefaultBinding defaultBinding,
            string actionName,
            string keybindName,
            string? prevBindName = null
        )
        {
            this.DefaultBinding = defaultBinding;
            this.ActionName = actionName;
            this.KeybindName = keybindName;
            this.PreviousBindName = prevBindName;
        }

        public DefaultBinding DefaultBinding { get; }
        public string ActionName { get; }
        public string KeybindName { get; }
        public string? PreviousBindName { get; }

        internal VDefaultBinding CreateBinding() // May only be called after SRInput.PlayerActions constructor has run.
            => this.DefaultBinding.Initialized ? new VDefaultBinding(
                    bindTo: Reg.actions[this],
                    primKey: (Key)this.DefaultBinding.PrimaryKey!,
                    secKey: (Key)this.DefaultBinding.SecondaryKey!,
                    primBtn: (Btn)this.DefaultBinding.PrimaryButton!,
                    secBtn: (Btn)this.DefaultBinding.SecondaryButton!,
                    tertBtn: (Btn)this.DefaultBinding.TertiaryButton!
                ) : throw new ArgumentNullException($"Invalid {nameof(KeybindLib.DefaultBinding)}!");
    }
}
