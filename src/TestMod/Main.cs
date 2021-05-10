using System;
using SRML;

namespace TestMod
{
    public class Main : ModEntryPoint // This mod's entry point.
    {
        public override void PreLoad()
        {
            Keybinds.Register(); // The register method must be called during PreLoad.

            // ...
        }

        public override void Load()
        {
            if (Keybinds.Test.Action.IsPressed) // `Keybind.Action` can only be accessed after the PreLoad step.
            {
                Console.WriteLine("Hello World!");
            }

            // ...
        }
    }
}
