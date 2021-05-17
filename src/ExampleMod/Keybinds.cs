using System.Collections.Generic;
using InControl;
using KeybindLib;
using SRML.Console;                                                 // Just for printing output.
using static MessageDirector;
using Button = InControl.InputControlType;                          // Makes things more clear.

namespace ExampleMod
{
    public static class Keybinds                                    // A class for holding keybinds.
    {
        public static Keybind Test { get; }                         // A property to hold this keybind.
            = new Keybind(                                          // Create a new keybind.
                name: "key.test",                                   // The name of this keybind, must start with `key.`.
                comesBefore: "key.reportissue",                     // The key that this keybind should come immidiately before in the list. Can be `Keybind.BEGINNING_OF_LIST` for the start of the list. Optional.

                defaultBindings: new Bind[]                         // This keybind's default bindings, in the order they should be applied. Optional.
                {
                    Key.T                                           // Can be of type `InControl.Key`, `InControl.Mouse`, or `InControl.InputControlType`.
                },

                translations: new Dictionary<Lang, string>          // This keybind's translations. Optional.
                {
                    [Lang.EN] = "Test",                             // If the Translation API isn't installed, only `Lang.EN` will be used.
                    [Lang.ES] = "Probar",                           // WARNING: Translation API support is currently broken and has been disabled!
                },

                keyPressed: (_) =>                                  // This will run when the key is pressed.
                {                                                   // The lambda's parameter is `player` - here it uses a discard (`_`) because we're not using the parameter.
                    Console.Log("Test pressed!");
                },

                keyReleased: (_) =>                                 // This will run when the key is released.
                {
                    Console.Log("Test released!");
                },

                keyDownUpdate: (_) =>                               // This will run every frame that they key is down.
                {                                                   // Be careful not to put too much stuff in here, or it could slow down the game.
                    Console.Log("Test is down!");
                }                                                   // Those three actions can have things added to and removed from them dynamically via the keybind's corresponding event.
            );

        #region examples

        public static Keybind Jest { get; }                         // More example keybinds showing additional options.
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
                },

                keyPressed: (player) =>
                {
                    player.Jetpack.TryStart();
                },

                keyReleased: (player) =>
                {
                    player.Jetpack.TryStop();
                }
            );

        public static Keybind Kest { get; }
            = new Keybind(
                name: "key.kest",
                comesBefore: "key.screenshot",

                defaultBindings: new Bind[]
                {
                    Key.K,
                    Key.L
                }
            );

        public static Keybind Vest { get; }
            = new Keybind(
                name: "key.vest",
                comesBefore: "key.left",

                defaultBinding: Key.V,

                translation: "Vest",

                keyReleased: (player) =>
                {
                    player.Run.TryStart();
                }
            );

        public static Keybind Nest { get; }
            = new Keybind(
                name: "key.nest",
                comesBefore: Keybind.BEGINNING_OF_LIST,

                defaultBindings: new Bind[]
                {
                    Key.N,
                    Button.Action7
                },

                translations: new Dictionary<Lang, string>
                {
                    [Lang.EN] = "Nest",
                    [Lang.ES] = "Nido",
                    [Lang.KO] = "둥지"
                },

                keyDownUpdate: (player) =>
                {
                    player.Jump.TryStart();
                }
            );

        #endregion

        internal static void Register()                             // A method for registering all keybinds. Must be called during PreLoad.
        {
            KeybindRegistry.Register(new[]
            {
                Keybinds.Test,                                      // Each keybind to register.
                Keybinds.Jest,
                Keybinds.Kest,
                Keybinds.Vest,
                Keybinds.Nest
            });

            KeybindRegistry.Update += (_) =>                        // Add an action to run every frame (when accepting input).
            {
                if (Keybinds.Test.Action?.WasRepeated ?? false)     // `Keybind.Action` is nullable, because it isn't present before PreLoad. To be safe, use `?.` instead of `!.` or `.` in `KeybindRegistry.Update` handlers.
                {                                                   // You don't need to worry about `Keybind.Action` being null in the keybind's own events, as those will only be called if it's non-null.
                    Console.Log("Test repeated!");
                }
            };
        }
    }
}
