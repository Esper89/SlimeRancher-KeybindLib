# KeybindLib
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/Esper89/SlimeRancher-KeybindLib/Mono)](https://github.com/Esper89/SlimeRancher-KeybindLib/actions/workflows/mono.yml) [![Codacy Badge](https://app.codacy.com/project/badge/Grade/a2786a4c9aa14e9c98e047461a64b8a1)](https://www.codacy.com/gh/Esper89/SlimeRancher-KeybindLib/dashboard) [![Nonexistent Tests](https://img.shields.io/badge/tests-none-critical)]()

KeybindLib is a Slime Rancher library mod for adding new keybinds.

**WARNING: KeybindLib is still in the process of being tested right now and is in pre-release.**

## Usage

### End Users

If you want to use a mod that requires KeyebindLib, just place `KeybindLib.dll` in your SRML mods folder (`SRML/Mods`).

#### Releases

You can download KeybindLib from the [releases page](https://github.com/Esper89/SlimeRancher-KeybindLib/releases/latest) (below the changelog).

#### Mod Conflicts

KeybindLib should not have any conflicts with existing mods, except **possibly the crouching mod**. Any new mods that also add their own keybinds **may be incompatible** if they don't use KeybindLib.

### Modders

The following is for modders who want to use KeybindLib in your mod. Keep in mind that if your mod uses KeybindLib, anyone using your mod needs to have KeybindLib installed.

#### References

You need to add `KeybindLib.dll` as a reference to your project, the same way you would add `SRML.dll` as described [on the wiki](https://github.com/veesusmikelheir/SRML/wiki/Project-Setup#importing-the-references). It's highly recommended to also have `KeybindLib.xml` in the same directory as `KeybindLib.dll`, as it will allow you to see KeybindLib's XML documentation whil you're programming.

You will also need `InControl.dll` as a reference, because KeybindLib uses a few of the types from it.

#### modinfo.json

You also need to add this mod as a dependency of your mod, and you'll need to add it to your mod's `load_before` list. **Do not add this mod to your `load_after` list, it will not work.** Here's an example of a `modinfo.json` that's set up to use KeybindLib.

```json
{
  "id": "dashing",
  "name": "Dashing",
  "author": "Esper89",
  "description": "Allows you to air-dash!",
  "version": "1.0",
  "dependencies": [
    "keybindlib"
  ],
  "load_before": [
    "keybindlib"
  ]
}
```

If your mod can function without KeybindLib despite using some of it's features, you don't need to add it as a dependency, but it still needs to be in your mod's `load_before` list.

#### API

KeybindLib's API is documented by XML comments throughout the code, which are automatically converted to markdown and committed to [`src/KeybindLib/README.md`](./src/KeybindLib/README.md). You can check that for a quick reference. If you just want to learn how to use KeybindLib, see below for a small example.

```cs
using InControl;
using KeybindLib;
using SRML;
using SRML.Console;
using static MessageDirector;

public class Main : ModEntryPoint                       // Your mod's entry point.
{
    public override void PreLoad()                      // Keybinds must be set up during PreLoad.
    {
        Keybind hello = new Keybind(                    // Create a new keybind.
            name: "key.hello",                          // The name of this keybind. Must have `key.` prefix.
            comesBefore: "key.map",                     // The keybind that this one appears directly before in the keybind list.
            defaultBinding: Key.H,                      // The default binding for this keybind.
            translation: "Hello"                        // The english translation for this keybind.
            keyPressed: (player) =>                     // A delegate to run when this key is pressed.
            {
                Console.Log("Hello World!");            // This code will run every time the key is pressed.
            }
        );

        KeybindRegistry.Register(hello);                // Register the keybind.
    }
}
```

KeybindLib has other useful features, like multiple default keybinds, multiple translations, `keyReleased` and `keyDownUpdate`, and more. To see examples of all of them, check out [`src/ExampleMod`](./src/ExampleMod). Every KeybindLib release comes with an `ExampleMod.zip` bundled with it.

If you have any questions, feel free to message me on Discord (`Esper#8989`) or matrix (`@esper89:matrix.org`).

## Features

 - Adding new keybinds.

 - Controlling your keybind's position in the keybind list.

 - Setting any number of default bindings for your keybind.

 - Automatically registering translations.

 - ~~Translation API support, for multiple languages.~~ **([CURRENTLY BROKEN](https://github.com/Esper89/SlimeRancher-KeybindLib/issues/1))**

 - Events that run on key press, key release, and every frame that they key is down.

 - Attaching delegates to vanilla actions.

 - An event that runs every frame that keybinds are updated.

 - A HashSet of all vanilla keybinds and their internal names.

 - An example/test mod for easy testing and learning.

 - Very shiny transpilers that I'm very proud of.

### Limitations

 - May not be compatible with mods that manually add their own keybinds.

## Contributing

Contributions of any kind - issues, pull requests, feature requests - are all welcome. You can submit suggestions and bug reports [as an issue](https://github.com/Esper89/SlimeRancher-KeybindLib/issues/new/choose), or code contributions [as a pull request](https://github.com/Esper89/SlimeRancher-KeybindLib/pulls).

### Scope

Anything to do with keybinds can be considered within the scope of KeybindLib.

Also, KeybindLib is a library mod, so it doesn't change any gameplay mechanics on it's own.

### Building

KeybindLib's build system is in the [`build`](./build) directory. See [`build/README.md`](./build/README.md) for a guide to building KeybindLib.

## License

    Copyright (c) 2021 Esper Thomson

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
