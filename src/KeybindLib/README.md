<a name='assembly'></a>
# KeybindLib

## Contents

- [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind')
  - [BindDefault(action)](#M-KeybindLib-Bind-BindDefault-InControl-PlayerAction- 'KeybindLib.Bind.BindDefault(InControl.PlayerAction)')
  - [op_Implicit()](#M-KeybindLib-Bind-op_Implicit-InControl-Key-~KeybindLib-Bind 'KeybindLib.Bind.op_Implicit(InControl.Key)~KeybindLib.Bind')
  - [op_Implicit()](#M-KeybindLib-Bind-op_Implicit-InControl-InputControlType-~KeybindLib-Bind 'KeybindLib.Bind.op_Implicit(InControl.InputControlType)~KeybindLib.Bind')
  - [op_Implicit()](#M-KeybindLib-Bind-op_Implicit-InControl-Mouse-~KeybindLib-Bind 'KeybindLib.Bind.op_Implicit(InControl.Mouse)~KeybindLib.Bind')
- [ButtonBind](#T-KeybindLib-Bind-ButtonBind 'KeybindLib.Bind.ButtonBind')
  - [#ctor()](#M-KeybindLib-Bind-ButtonBind-#ctor-InControl-InputControlType- 'KeybindLib.Bind.ButtonBind.#ctor(InControl.InputControlType)')
  - [BindDefault()](#M-KeybindLib-Bind-ButtonBind-BindDefault-InControl-PlayerAction- 'KeybindLib.Bind.ButtonBind.BindDefault(InControl.PlayerAction)')
- [KeyBind](#T-KeybindLib-Bind-KeyBind 'KeybindLib.Bind.KeyBind')
  - [#ctor()](#M-KeybindLib-Bind-KeyBind-#ctor-InControl-Key- 'KeybindLib.Bind.KeyBind.#ctor(InControl.Key)')
  - [BindDefault()](#M-KeybindLib-Bind-KeyBind-BindDefault-InControl-PlayerAction- 'KeybindLib.Bind.KeyBind.BindDefault(InControl.PlayerAction)')
- [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind')
  - [#ctor(name,defaultBindings,comesBefore,translations)](#M-KeybindLib-Keybind-#ctor-System-String,KeybindLib-Bind[],System-String,System-Collections-Generic-Dictionary{MessageDirector-Lang,System-String}- 'KeybindLib.Keybind.#ctor(System.String,KeybindLib.Bind[],System.String,System.Collections.Generic.Dictionary{MessageDirector.Lang,System.String})')
  - [#ctor(name,translation,defaultBindings,comesBefore)](#M-KeybindLib-Keybind-#ctor-System-String,System-String,KeybindLib-Bind[],System-String- 'KeybindLib.Keybind.#ctor(System.String,System.String,KeybindLib.Bind[],System.String)')
  - [BEGINNING_OF_LIST](#F-KeybindLib-Keybind-BEGINNING_OF_LIST 'KeybindLib.Keybind.BEGINNING_OF_LIST')
  - [KEYBIND_PREFIX](#F-KeybindLib-Keybind-KEYBIND_PREFIX 'KeybindLib.Keybind.KEYBIND_PREFIX')
  - [Action](#P-KeybindLib-Keybind-Action 'KeybindLib.Keybind.Action')
  - [ActionName](#P-KeybindLib-Keybind-ActionName 'KeybindLib.Keybind.ActionName')
  - [ComesBefore](#P-KeybindLib-Keybind-ComesBefore 'KeybindLib.Keybind.ComesBefore')
  - [DefaultBindings](#P-KeybindLib-Keybind-DefaultBindings 'KeybindLib.Keybind.DefaultBindings')
  - [Name](#P-KeybindLib-Keybind-Name 'KeybindLib.Keybind.Name')
  - [Translations](#P-KeybindLib-Keybind-Translations 'KeybindLib.Keybind.Translations')
  - [BindAllDefaultsTo(action)](#M-KeybindLib-Keybind-BindAllDefaultsTo-InControl-PlayerAction- 'KeybindLib.Keybind.BindAllDefaultsTo(InControl.PlayerAction)')
  - [RegisterTranslations()](#M-KeybindLib-Keybind-RegisterTranslations 'KeybindLib.Keybind.RegisterTranslations')
- [KeybindInvalidException](#T-KeybindLib-KeybindRegistry-KeybindInvalidException 'KeybindLib.KeybindRegistry.KeybindInvalidException')
  - [Keybind](#P-KeybindLib-KeybindRegistry-KeybindInvalidException-Keybind 'KeybindLib.KeybindRegistry.KeybindInvalidException.Keybind')
- [KeybindNotReadyException](#T-KeybindLib-Keybind-KeybindNotReadyException 'KeybindLib.Keybind.KeybindNotReadyException')
  - [#ctor(keybind)](#M-KeybindLib-Keybind-KeybindNotReadyException-#ctor-KeybindLib-Keybind- 'KeybindLib.Keybind.KeybindNotReadyException.#ctor(KeybindLib.Keybind)')
  - [Keybind](#P-KeybindLib-Keybind-KeybindNotReadyException-Keybind 'KeybindLib.Keybind.KeybindNotReadyException.Keybind')
- [KeybindRegisteredTooLateException](#T-KeybindLib-KeybindRegistry-KeybindRegisteredTooLateException 'KeybindLib.KeybindRegistry.KeybindRegisteredTooLateException')
  - [Keybind](#P-KeybindLib-KeybindRegistry-KeybindRegisteredTooLateException-Keybind 'KeybindLib.KeybindRegistry.KeybindRegisteredTooLateException.Keybind')
- [KeybindRegistry](#T-KeybindLib-KeybindRegistry 'KeybindLib.KeybindRegistry')
  - [Register(keybind)](#M-KeybindLib-KeybindRegistry-Register-KeybindLib-Keybind- 'KeybindLib.KeybindRegistry.Register(KeybindLib.Keybind)')
  - [Register(keybinds)](#M-KeybindLib-KeybindRegistry-Register-System-Collections-Generic-IEnumerable{KeybindLib-Keybind}- 'KeybindLib.KeybindRegistry.Register(System.Collections.Generic.IEnumerable{KeybindLib.Keybind})')
- [Main](#T-KeybindLib-Main 'KeybindLib.Main')
  - [PreLoad()](#M-KeybindLib-Main-PreLoad 'KeybindLib.Main.PreLoad')
- [MethodKeybindExtractor](#T-KeybindLib-MethodKeybindExtractor 'KeybindLib.MethodKeybindExtractor')
  - [VanillaKeybinds](#P-KeybindLib-MethodKeybindExtractor-VanillaKeybinds 'KeybindLib.MethodKeybindExtractor.VanillaKeybinds')
- [MouseBind](#T-KeybindLib-Bind-MouseBind 'KeybindLib.Bind.MouseBind')
  - [#ctor()](#M-KeybindLib-Bind-MouseBind-#ctor-InControl-Mouse- 'KeybindLib.Bind.MouseBind.#ctor(InControl.Mouse)')
  - [BindDefault()](#M-KeybindLib-Bind-MouseBind-BindDefault-InControl-PlayerAction- 'KeybindLib.Bind.MouseBind.BindDefault(InControl.PlayerAction)')

<a name='T-KeybindLib-Bind'></a>
## Bind `type`

##### Namespace

KeybindLib

##### Summary

A binding.

##### Remarks

Represents the various types of bindings accepted by [](#N-InControl 'InControl').

<a name='M-KeybindLib-Bind-BindDefault-InControl-PlayerAction-'></a>
### BindDefault(action) `method`

##### Summary

Adds this instance as a default binding for the specified [PlayerAction](#T-InControl-PlayerAction 'InControl.PlayerAction').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [InControl.PlayerAction](#T-InControl-PlayerAction 'InControl.PlayerAction') | The [PlayerAction](#T-InControl-PlayerAction 'InControl.PlayerAction') to bind to. |

<a name='M-KeybindLib-Bind-op_Implicit-InControl-Key-~KeybindLib-Bind'></a>
### op_Implicit() `method`

##### Summary

Converts a [Key](#T-InControl-Key 'InControl.Key') to a [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind').

##### Parameters

This method has no parameters.

<a name='M-KeybindLib-Bind-op_Implicit-InControl-InputControlType-~KeybindLib-Bind'></a>
### op_Implicit() `method`

##### Summary

Converts a [InputControlType](#T-InControl-InputControlType 'InControl.InputControlType') to a [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind').

##### Parameters

This method has no parameters.

<a name='M-KeybindLib-Bind-op_Implicit-InControl-Mouse-~KeybindLib-Bind'></a>
### op_Implicit() `method`

##### Summary

Converts a [Mouse](#T-InControl-Mouse 'InControl.Mouse') to a [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind').

##### Parameters

This method has no parameters.

<a name='T-KeybindLib-Bind-ButtonBind'></a>
## ButtonBind `type`

##### Namespace

KeybindLib.Bind

##### Summary

A [InputControlType](#T-InControl-InputControlType 'InControl.InputControlType')[Bind](#T-KeybindLib-Bind 'KeybindLib.Bind').

<a name='M-KeybindLib-Bind-ButtonBind-#ctor-InControl-InputControlType-'></a>
### #ctor() `constructor`

##### Summary

Creates a new [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind') from a [InputControlType](#T-InControl-InputControlType 'InControl.InputControlType').

##### Parameters

This constructor has no parameters.

<a name='M-KeybindLib-Bind-ButtonBind-BindDefault-InControl-PlayerAction-'></a>
### BindDefault() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-KeybindLib-Bind-KeyBind'></a>
## KeyBind `type`

##### Namespace

KeybindLib.Bind

##### Summary

A [Key](#T-InControl-Key 'InControl.Key')[Bind](#T-KeybindLib-Bind 'KeybindLib.Bind').

<a name='M-KeybindLib-Bind-KeyBind-#ctor-InControl-Key-'></a>
### #ctor() `constructor`

##### Summary

Creates a new [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind') from a [Key](#T-InControl-Key 'InControl.Key').

##### Parameters

This constructor has no parameters.

<a name='M-KeybindLib-Bind-KeyBind-BindDefault-InControl-PlayerAction-'></a>
### BindDefault() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-KeybindLib-Keybind'></a>
## Keybind `type`

##### Namespace

KeybindLib

##### Summary

A keybind.

##### See Also

- [KeybindLib.KeybindRegistry](#T-KeybindLib-KeybindRegistry 'KeybindLib.KeybindRegistry')

<a name='M-KeybindLib-Keybind-#ctor-System-String,KeybindLib-Bind[],System-String,System-Collections-Generic-Dictionary{MessageDirector-Lang,System-String}-'></a>
### #ctor(name,defaultBindings,comesBefore,translations) `constructor`

##### Summary

Creates a new [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of this instance. |
| defaultBindings | [KeybindLib.Bind[]](#T-KeybindLib-Bind[] 'KeybindLib.Bind[]') | The default keybinds that apply to this instance. |
| comesBefore | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The keybind that this one should come before. |
| translations | [System.Collections.Generic.Dictionary{MessageDirector.Lang,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{MessageDirector.Lang,System.String}') | The translations that apply to this instance. |

<a name='M-KeybindLib-Keybind-#ctor-System-String,System-String,KeybindLib-Bind[],System-String-'></a>
### #ctor(name,translation,defaultBindings,comesBefore) `constructor`

##### Summary

Creates a new [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of this instance. |
| translation | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The english translation for this instance. |
| defaultBindings | [KeybindLib.Bind[]](#T-KeybindLib-Bind[] 'KeybindLib.Bind[]') | The default keybinds that apply to this instance. |
| comesBefore | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The keybind that this one should come before. |

<a name='F-KeybindLib-Keybind-BEGINNING_OF_LIST'></a>
### BEGINNING_OF_LIST `constants`

##### Summary

A special string that can be used as [ComesBefore](#P-KeybindLib-Keybind-ComesBefore 'KeybindLib.Keybind.ComesBefore') to represent the beginning of the list.

<a name='F-KeybindLib-Keybind-KEYBIND_PREFIX'></a>
### KEYBIND_PREFIX `constants`

##### Summary

The expected prefix that every keybind's name should have.

<a name='P-KeybindLib-Keybind-Action'></a>
### Action `property`

##### Summary

This instance's .

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [KeybindLib.Keybind.KeybindNotReadyException](#T-KeybindLib-Keybind-KeybindNotReadyException 'KeybindLib.Keybind.KeybindNotReadyException') | Thrown when this is accessed before the Load step. |

<a name='P-KeybindLib-Keybind-ActionName'></a>
### ActionName `property`

##### Summary

The name of this keybind's corresponding [PlayerAction](#T-InControl-PlayerAction 'InControl.PlayerAction').

<a name='P-KeybindLib-Keybind-ComesBefore'></a>
### ComesBefore `property`

##### Summary

The name of the vanilla keybind that should come after this instance.

##### Remarks

Indicates where in the list of keybinds this one should go.

##### See Also

- [KeybindLib.MethodKeybindExtractor.VanillaKeybinds](#P-KeybindLib-MethodKeybindExtractor-VanillaKeybinds 'KeybindLib.MethodKeybindExtractor.VanillaKeybinds')
- [KeybindLib.Keybind.BEGINNING_OF_LIST](#F-KeybindLib-Keybind-BEGINNING_OF_LIST 'KeybindLib.Keybind.BEGINNING_OF_LIST')

<a name='P-KeybindLib-Keybind-DefaultBindings'></a>
### DefaultBindings `property`

##### Summary

The default [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind')s for this instance.

<a name='P-KeybindLib-Keybind-Name'></a>
### Name `property`

##### Summary

This instance's name.

##### Remarks

Must start with "key.".

<a name='P-KeybindLib-Keybind-Translations'></a>
### Translations `property`

##### Summary

All translations that apply to this instance.

##### Remarks

If the Translatio API mod is not installed, only [EN](#F-MessageDirector-Lang-EN 'MessageDirector.Lang.EN') will apply.

<a name='M-KeybindLib-Keybind-BindAllDefaultsTo-InControl-PlayerAction-'></a>
### BindAllDefaultsTo(action) `method`

##### Summary

Binds all [DefaultBindings](#P-KeybindLib-Keybind-DefaultBindings 'KeybindLib.Keybind.DefaultBindings') of this instance to the given [PlayerAction](#T-InControl-PlayerAction 'InControl.PlayerAction').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [InControl.PlayerAction](#T-InControl-PlayerAction 'InControl.PlayerAction') | The [PlayerAction](#T-InControl-PlayerAction 'InControl.PlayerAction') to bind to. |

<a name='M-KeybindLib-Keybind-RegisterTranslations'></a>
### RegisterTranslations() `method`

##### Summary

Registers this instance's [Translations](#P-KeybindLib-Keybind-Translations 'KeybindLib.Keybind.Translations'), with either the Translation API or SRML.

##### Parameters

This method has no parameters.

##### Remarks

Only registers english translations if the Translation API isn't available.

<a name='T-KeybindLib-KeybindRegistry-KeybindInvalidException'></a>
## KeybindInvalidException `type`

##### Namespace

KeybindLib.KeybindRegistry

##### Summary

An exception thrown when an invalid [Keybind](#P-KeybindLib-KeybindRegistry-KeybindInvalidException-Keybind 'KeybindLib.KeybindRegistry.KeybindInvalidException.Keybind') is registered.

<a name='P-KeybindLib-KeybindRegistry-KeybindInvalidException-Keybind'></a>
### Keybind `property`

##### Summary

The invalid [Keybind](#P-KeybindLib-KeybindRegistry-KeybindInvalidException-Keybind 'KeybindLib.KeybindRegistry.KeybindInvalidException.Keybind') in question.

<a name='T-KeybindLib-Keybind-KeybindNotReadyException'></a>
## KeybindNotReadyException `type`

##### Namespace

KeybindLib.Keybind

##### Summary

An exception thrown when [Action](#P-KeybindLib-Keybind-Action 'KeybindLib.Keybind.Action') is accessed before it is ready.

<a name='M-KeybindLib-Keybind-KeybindNotReadyException-#ctor-KeybindLib-Keybind-'></a>
### #ctor(keybind) `constructor`

##### Summary

Creates a new [KeybindNotReadyException](#T-KeybindLib-Keybind-KeybindNotReadyException 'KeybindLib.Keybind.KeybindNotReadyException').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keybind | [KeybindLib.Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind') | The offending keybind. |

<a name='P-KeybindLib-Keybind-KeybindNotReadyException-Keybind'></a>
### Keybind `property`

##### Summary

The [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind') whose [Action](#P-KeybindLib-Keybind-Action 'KeybindLib.Keybind.Action') was accessed before it was ready.

<a name='T-KeybindLib-KeybindRegistry-KeybindRegisteredTooLateException'></a>
## KeybindRegisteredTooLateException `type`

##### Namespace

KeybindLib.KeybindRegistry

##### Summary

An exception thrown when a [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind') is registered after this mod has been PreLoaded.

<a name='P-KeybindLib-KeybindRegistry-KeybindRegisteredTooLateException-Keybind'></a>
### Keybind `property`

##### Summary

The [Keybind](#P-KeybindLib-KeybindRegistry-KeybindRegisteredTooLateException-Keybind 'KeybindLib.KeybindRegistry.KeybindRegisteredTooLateException.Keybind') that was registered too late.

<a name='T-KeybindLib-KeybindRegistry'></a>
## KeybindRegistry `type`

##### Namespace

KeybindLib

##### Summary

Registers [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind')s and keeps track of metadata.

<a name='M-KeybindLib-KeybindRegistry-Register-KeybindLib-Keybind-'></a>
### Register(keybind) `method`

##### Summary

Registers a [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keybind | [KeybindLib.Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind') | The [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind') to register. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [KeybindLib.KeybindRegistry.KeybindInvalidException](#T-KeybindLib-KeybindRegistry-KeybindInvalidException 'KeybindLib.KeybindRegistry.KeybindInvalidException') | Thrown when `keybind` has an invalid [Name](#P-KeybindLib-Keybind-Name 'KeybindLib.Keybind.Name') or [ComesBefore](#P-KeybindLib-Keybind-ComesBefore 'KeybindLib.Keybind.ComesBefore'). |
| [KeybindLib.KeybindRegistry.KeybindRegisteredTooLateException](#T-KeybindLib-KeybindRegistry-KeybindRegisteredTooLateException 'KeybindLib.KeybindRegistry.KeybindRegisteredTooLateException') | Thrown when this method is called after PreLoad. |

##### Remarks

Registered keybinds must have a unique [Name](#P-KeybindLib-Keybind-Name 'KeybindLib.Keybind.Name') and a valid [ComesBefore](#P-KeybindLib-Keybind-ComesBefore 'KeybindLib.Keybind.ComesBefore').

##### See Also

- [KeybindLib.KeybindRegistry.Register](#M-KeybindLib-KeybindRegistry-Register-System-Collections-Generic-IEnumerable{KeybindLib-Keybind}- 'KeybindLib.KeybindRegistry.Register(System.Collections.Generic.IEnumerable{KeybindLib.Keybind})')

<a name='M-KeybindLib-KeybindRegistry-Register-System-Collections-Generic-IEnumerable{KeybindLib-Keybind}-'></a>
### Register(keybinds) `method`

##### Summary

*Inherit from parent.*

##### Summary

Registers a collection of [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind')s.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keybinds | [System.Collections.Generic.IEnumerable{KeybindLib.Keybind}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{KeybindLib.Keybind}') | The [Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind')s to register. |

##### See Also

- [KeybindLib.KeybindRegistry.Register](#M-KeybindLib-KeybindRegistry-Register-KeybindLib-Keybind- 'KeybindLib.KeybindRegistry.Register(KeybindLib.Keybind)')

<a name='T-KeybindLib-Main'></a>
## Main `type`

##### Namespace

KeybindLib

##### Summary

This mod's entry point.

##### See Also

- [KeybindLib.Keybind](#T-KeybindLib-Keybind 'KeybindLib.Keybind')
- [KeybindLib.KeybindRegistry](#T-KeybindLib-KeybindRegistry 'KeybindLib.KeybindRegistry')

<a name='M-KeybindLib-Main-PreLoad'></a>
### PreLoad() `method`

##### Summary

Applies all patches. Keybinds must be registered before this is called.

##### Parameters

This method has no parameters.

##### Remarks

Add "keybindlib" to your mod's "load_after" list to ensure this is called after your keybinds are registered.

##### See Also

- [KeybindLib.KeybindRegistry](#T-KeybindLib-KeybindRegistry 'KeybindLib.KeybindRegistry')

<a name='T-KeybindLib-MethodKeybindExtractor'></a>
## MethodKeybindExtractor `type`

##### Namespace

KeybindLib

##### Summary

Parses vanilla methods to extract keybind information.

<a name='P-KeybindLib-MethodKeybindExtractor-VanillaKeybinds'></a>
### VanillaKeybinds `property`

##### Summary

The set of all valid vanilla keybind names.

##### Remarks

Mutating this object has no effect. Includes .

<a name='T-KeybindLib-Bind-MouseBind'></a>
## MouseBind `type`

##### Namespace

KeybindLib.Bind

##### Summary

A [Mouse](#T-InControl-Mouse 'InControl.Mouse')[Bind](#T-KeybindLib-Bind 'KeybindLib.Bind').

<a name='M-KeybindLib-Bind-MouseBind-#ctor-InControl-Mouse-'></a>
### #ctor() `constructor`

##### Summary

Creates a new [Bind](#T-KeybindLib-Bind 'KeybindLib.Bind') from a [Mouse](#T-InControl-Mouse 'InControl.Mouse').

##### Parameters

This constructor has no parameters.

<a name='M-KeybindLib-Bind-MouseBind-BindDefault-InControl-PlayerAction-'></a>
### BindDefault() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.
