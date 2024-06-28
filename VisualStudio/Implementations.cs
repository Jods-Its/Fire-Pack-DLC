using FirePack;
using MelonLoader;

namespace ModNamespace;
internal sealed class Implementations : MelonMod
{
	public override void OnInitializeMelon()
	{
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Starting fires...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Picking embers...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Placing firelogs...");
        MelonLoader.MelonLogger.Msg(System.ConsoleColor.Green, "Fire Pack 2.7.1 Loaded!");
        Settings.instance.AddToModSettings("Fire Pack");
    }
}
