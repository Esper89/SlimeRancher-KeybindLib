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

using System.Collections.Generic;
using System.Linq;
using InControl;
#if TRSL_API
using SRML;
#endif
using SRML.SR;
using static MessageDirector;

namespace KeybindLib
{
    /// <summary> A keybind. </summary>
    /// <seealso cref="KeybindRegistry"/>
    public class Keybind
    {
        /// <summary> Creates a new <see cref="Keybind"/>. </summary>
        /// <param name="name"> The name of this instance. </param>
        /// <param name="defaultBindings"> The default keybinds that apply to this instance. </param>
        /// <param name="comesBefore"> The keybind that this one should come before. </param>
        /// <param name="translations"> The translations that apply to this instance. </param>
        /// <param name="keyPressed"> The <see cref="KeyAction"/> to run when this key is pressed. </param>
        /// <param name="keyReleased"> The <see cref="KeyAction"/> to run when this key is released. </param>
        /// <param name="keyDownUpdate"> The <see cref="KeyAction"/> to run every frame if this key is down. </param>
        public Keybind(
            string name,
            Bind[]? defaultBindings = null,
            string? comesBefore = null,
            Dictionary<Lang, string>? translations = null,
            KeyAction? keyPressed = null,
            KeyAction? keyReleased = null,
            KeyAction? keyDownUpdate = null
        )
        {
            this.Name = name + Keybind.DEBUG_SUFFIX;
            this.DefaultBindings = defaultBindings ?? new Bind[] { };
            this.ComesBefore = comesBefore;
            this.Translations = translations ?? new Dictionary<Lang, string> { };
            this.KeyPressed = keyPressed ?? ((player) => { });
            this.KeyReleased = keyReleased ?? ((player) => { });
            this.KeyDownUpdate = keyDownUpdate ?? ((player) => { });
        }

        /// <summary> This instance's name. </summary>
        /// <remarks> Must start with `key.`. </remarks>
        public virtual string Name { get; }

        /// <summary> The default <see cref="Bind"/>s for this instance. </summary>
        public virtual Bind[] DefaultBindings { get; }

        /// <summary> The name of the vanilla keybind that should come after this instance. </summary>
        /// <remarks> Indicates where in the list of keybinds this one should go. </remarks>
        /// <seealso cref="MethodKeybindExtractor.VanillaKeybinds"/>
        /// <seealso cref="BEGINNING_OF_LIST"/>
        public virtual string? ComesBefore { get; }

        /// <summary> All translations that apply to this instance. </summary>
        /// <remarks> If the Translatio API mod is not installed, only <see cref="Lang.EN"/> will apply. </remarks>
        public virtual Dictionary<Lang, string> Translations { get; }

        /// <summary> The name of this keybind's corresponding <see cref="PlayerAction"/>. </summary>
        protected internal virtual string ActionName => $"Action:{this.Name}";

        /// <summary> A special string that can be used as <see cref="ComesBefore"/> to represent the beginning of the list. </summary>
        public const string BEGINNING_OF_LIST = "BEGINNING";

        /// <summary> The expected prefix that every keybind's name should have. </summary>
        public const string KEYBIND_PREFIX = "key.";

#if DEBUG
        internal const string DEBUG_SUFFIX = ":DEBUG"; // Makes the keybind stand out in the list, for easier debugging.
#else
        internal const string DEBUG_SUFFIX = ""; // Empty, to be unintrusive.
#endif

        #region events

        /// <summary> A delegate that runs when something interesting happens with a keybind. </summary>
        /// <remarks> Event handler. </remarks>
        /// <seealso cref="KeyPressed"/>
        /// <seealso cref="KeyReleased"/>
        /// <see cref="KeyDownUpdate"/>
        public delegate void KeyAction(vp_FPPlayerEventHandler player);

        /// <summary> Occurs when this instance's key is pressed. </summary>
        public event KeyAction KeyPressed;

        /// <summary> Occurs when this instance's key is released. </summary>
        public event KeyAction KeyReleased;

        /// <summary> Occurs every frame that this instance's key is down for. </summary>
        public event KeyAction KeyDownUpdate;

        internal void Update(vp_FPPlayerEventHandler player)
        {
            if (this.Action is null) return;

            if (this.Action.WasPressed)
            {
                this.KeyPressed(player);
            }

            if (this.Action.WasReleased)
            {
                this.KeyReleased(player);
            }

            if (this.Action.IsPressed)
            {
                this.KeyDownUpdate(player);
            }
        }

        #endregion

        /// <summary> This instance's <seealso cref="PlayerAction"/>. </summary>
        /// <remarks> Only null during PreLoad. </remarks>
        public PlayerAction? Action { get; internal set; }

        /// <summary> Binds all <see cref="DefaultBindings"/> of this instance to the given <see cref="PlayerAction"/>. </summary>
        /// <param name="action"> The <see cref="PlayerAction"/> to bind to. </param>
        protected internal virtual void BindAllDefaultsTo(PlayerAction action)
        {
            foreach (Bind binding in this.DefaultBindings)
            {
                binding.BindDefault(action);
            }
        }

        /// <summary> Registers this instance's <see cref="Translations"/>, with either the Translation API or SRML. </summary>
        /// <remarks> Only registers english translations if the Translation API isn't available. </remarks>
        protected internal virtual void RegisterTranslations()
        {
            // TODO Translation API Support
            // For some reason unknown to me, the translation API isn't working as expected.
            // I've disabled it until I can get it working. Define the `TRSL_API` symbol to enable it.

#if TRSL_API
            if (SRModLoader.IsModPresent("translationapi"))
            {
                foreach (KeyValuePair<Lang, string> translation in this.Translations)
                {
                    this.RegisterTranslationWithAPI(
                        language: translation.Key,
                        value: translation.Value + Keybind.DEBUG_SUFFIX
                    );
                }
            }
            else
            {
#endif
            if (this.Translations.TryGetValue(Lang.EN, out string value))
            {
                this.RegisterTranslationWithSRML(value + Keybind.DEBUG_SUFFIX);
            }
            else if (this.Translations.Keys.Count >= 1) // In case there's no English translation.
            {
                this.RegisterTranslationWithSRML(
                    this.Translations.Values.ToArray()[0] +
                    Keybind.DEBUG_SUFFIX
                );
            }
#if TRSL_API
            }
#endif
        }

        private void RegisterTranslationWithSRML(string value)
            => TranslationPatcher.AddUITranslation(this.Name, value);

#if TRSL_API
        private void RegisterTranslationWithAPI(Lang language, string value) // Separate method to prevent FileNotFoundException.
            => TranslationAPI.TranslationUtil.RegisterTranslation(
                selectedLang: language,
                bundle: TranslationAPI.LanguageController.UI_BUNDLE,
                key: this.Name,
                value: value
            );
#endif
    }
}
