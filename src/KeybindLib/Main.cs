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

using System.ComponentModel;
using SRML;

namespace KeybindLib
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
#if DEBUG
            HarmonyLib.Harmony.DEBUG = true;
#endif
            base.HarmonyInstance.PatchAll();
        }
    }
}
