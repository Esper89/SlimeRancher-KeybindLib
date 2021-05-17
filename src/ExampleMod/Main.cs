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
            {                                                       // This code won't actually run, as I don't think the game starts listening for keybinds until it's fully loaded.
                Console.Log("Hello World from KeybindLib!");
            }

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
