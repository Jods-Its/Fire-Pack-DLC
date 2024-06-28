using ModSettings;

namespace FirePack
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Section("Difficulty Settings")]
        [Name("Pull Torches From Fire")]
        [Description("Let you get torches from fires. Default = Yes")]
        public bool pullTorches = true;

        [Name("Consume Torch On Firestart")]
        [Description("When starting a fire with a lit torch, it will be consumed in the process. Default = No")]
        public bool consumeTorchOnFirestart = false;

        [Name("No Wood Matches Anywhere (Challenge)")]
        [Description("The player starts with a set of pack matches and cannot obtain any wood matches again. After the pack matches are consumed, the player must use renewable firestarting tools. Warning: turning this on will delete your wood matches. Default = No")]
        public bool noWoodMatches = false;

    }
}
