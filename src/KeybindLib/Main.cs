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
    /// <summary> This mod's entry point. </summary>
    /// <seealso cref="Keybind"/>
    /// <seealso cref="KeybindRegistry"/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Main : ModEntryPoint
    {
        /// <summary> Applies all patches. Keybinds must be registered before this is called. </summary>
        /// <remarks> Add "keybindlib" to your mod's "load_after" list to ensure this is called after your keybinds are registered. </remarks>
        /// <seealso cref="KeybindRegistry"/>
        public override void PreLoad()
        {
#if DEBUG
            HarmonyLib.Harmony.DEBUG = true;
#endif
            base.HarmonyInstance.PatchAll();

            KeybindRegistry.RegisterTranslations();

            Main.hasPreloaded = true; // Lets the keybind registry know that it can't accept new keybinds.
        }

        internal static bool hasPreloaded = false;
    }
}
