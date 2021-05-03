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
using Btn = InControl.InputControlType;

namespace KeybindLib
{
    public struct DefaultBinding
    {
        public DefaultBinding(
            Key? primKey = null,
            Key? secKey = null,
            Btn? primBtn = null,
            Btn? secBtn = null,
            Btn? tertBtn = null
        )
        {
            this.PrimaryKey = primKey ?? Key.None;
            this.SecondaryKey = secKey ?? Key.None;
            this.PrimaryButton = primBtn ?? Btn.None;
            this.SecondaryButton = secBtn ?? Btn.None;
            this.TertiaryButton = tertBtn ?? Btn.None;
        }

        internal bool Initialized =>
                this.PrimaryKey is object &&
                this.SecondaryKey is object &&
                this.PrimaryButton is object &&
                this.SecondaryButton is object &&
                this.TertiaryButton is object;

        public Key? PrimaryKey { get; }
        public Key? SecondaryKey { get; }
        public Btn? PrimaryButton { get; }
        public Btn? SecondaryButton { get; }
        public Btn? TertiaryButton { get; }
    }
}
