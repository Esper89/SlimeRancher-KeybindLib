using KeybindLib;
using SRML;
using SRML.Console;

namespace ExampleMod
{
    public class Main : ModEntryPoint                               // This mod's entry point.
    {
        public override void PreLoad()
        {
            Keybinds.Register();                                    // The keybinds must be registered during PreLoad.

            // ...
        }

        public override void Load()
        {
            if (Keybinds.Test.Action!.IsPressed)                    // `Keybind.Action` will be null until after PreLoad. If you're not sure, use `?.` instead of `!.` to be safe.
            {                                                       // This code will never actually run, because I don't think the game starts listening for keybinds until after mods are loaded.
                Console.Log("Hello World from KeybindLib!");
            }

            KeybindRegistry.RegisterKeyAction(                      // Add an action to run when a `PlayerAction` is activated. For `PlayerAction`s that don't have a corresponding `Keybind`, like vanilla ones.
                SRInput.Actions.jump,                               // You can only access `SRInput.Actions.jump` after the PreLoad step, therefore you can only call `KeybindRegistry.RegisterKeyAction` after the PreLoad step.
                (player) =>                                         // They `KeyAction` to run.
                {
                    Console.Log("Thanks for jumping!");
                    player.Run.TryStart();
                },
                keyReleased: true                                   // Make it activate when the key is released instead of pressed.
            );

            // ...
        }

        public override void PostLoad()
        {
            foreach (string keybind in
                MethodKeybindExtractor.VanillaKeybinds              // A HashSet of all the vanilla keybinds and their internal names.
            )                                                       // This doesn't have to be in PostLoad, I just did it here for fun.
            {
                Console.Log(keybind);                               // Writes them to the console.
            }

            // ...
        }
    }
}
