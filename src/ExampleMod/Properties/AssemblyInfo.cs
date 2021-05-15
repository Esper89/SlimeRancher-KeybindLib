using System.Reflection;

[assembly: AssemblyTitle("KeybindLib.ExampleMod")]
[assembly: AssemblyCopyright("Esper Thomson")]
[assembly: AssemblyDescription("A Slime Rancher mod for testing KeybindLib.")]
[assembly: AssemblyVersion("0.1.*")]

#if DEBUG
[assembly: AssemblyConfiguration("DEBUG")]
#elif TESTING
[assembly: AssemblyConfiguration("TESTING")]
#endif
