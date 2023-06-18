using FirePack;
using MelonLoader;

namespace ModNamespace;
internal sealed class Implementations : MelonMod
{
	public override void OnInitializeMelon()
	{
        MelonLoader.MelonLogger.Msg("Starting fires...");
        MelonLoader.MelonLogger.Msg("Picking embers...");
        MelonLoader.MelonLogger.Msg("Placing firelogs...");
        MelonLoader.MelonLogger.Msg("Fire Pack Loaded!");
        Settings.instance.AddToModSettings("Fire Pack");
    }
}
