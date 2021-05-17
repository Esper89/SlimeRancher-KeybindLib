using System.Collections.Generic;
using InControl;
using KeybindLib;
using static MessageDirector;
using Button = InControl.InputControlType;

namespace ExampleMod
{
    public static class Keybinds // A class for holding keybinds.
    {
        public static Keybind Test { get; }                     // A property to hold this keybind.
            = new Keybind(                                      // Create a new keybind.
                name: "key.test",                               // The name of this keybind, must start with `key.`.
                defaultBindings: new Bind[]                     // This keybind's default bindings, in the order they should be applied. Optional.
                {                                               //
                    Key.T                                       // Can be of type `InControl.Key`, `InControl.Mouse`, or `InControl.InputControlType`.
                },                                              //
                comesBefore: "key.reportissue",                 // The key that this keybind should come immidiately before in the list. Can be `Keybind.BEGINNING_OF_LIST` for the start of the list. Optional.
                translations: new Dictionary<Lang, string>      // This keybind's translations. Optional.
                {                                               //
                    [Lang.EN] = "Test",                         // If the Translation API isn't installed, only `Lang.EN` will be used.
                    [Lang.ES] = "Probar",                       // WARNING: Translation API support is currently broken and has been disabled!
                }                                               //
            );

        public static Keybind Jest { get; }
            = new Keybind(
                name: "key.jest",
                defaultBindings: new Bind[]
                {
                    Key.J,
                    Mouse.Button9
                },
                translations: new Dictionary<Lang, string>
                {
                    [Lang.EN] = "Jest",
                    [Lang.ES] = "Bromear",
                    [Lang.FR] = "Plaisanter"
                }
            );

        public static Keybind Kest { get; }
            = new Keybind(
                name: "key.kest",
                defaultBindings: new Bind[]
                {
                    Key.K,
                    Key.L
                },
                comesBefore: "key.screenshot"
            );

        public static Keybind Vest { get; }
            = new Keybind(
                name: "key.vest",
                defaultBindings: new Bind[]
                {
                    Key.V
                },
                comesBefore: "key.left",
                translations: new Dictionary<Lang, string>
                {
                    [Lang.EN] = "Vest",
                    [Lang.FR] = "Gilet"
                }
            );

        public static Keybind Nest { get; }
            = new Keybind(
                name: "key.nest",
                defaultBindings: new Bind[]
                {
                    Key.N,
                    Button.Action7
                },
                comesBefore: Keybind.BEGINNING_OF_LIST,
                translations: new Dictionary<Lang, string>
                {
                    [Lang.EN] = "Nest",
                    [Lang.ES] = "Nido",
                    [Lang.KO] = "둥지"
                }
            );

        internal static void Register() // A method for registering all keybinds. Must be called during PreLoad.
        {
            KeybindRegistry.Register(new[]
            {
                Keybinds.Test, // Each keybind to register.
                Keybinds.Jest,
                Keybinds.Kest,
                Keybinds.Vest,
                Keybinds.Nest
            });
        }
    }
}
