using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using TwitchChatAPI;
using Unity.Netcode;

namespace TwitchCompany
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("TwitchChatAPI", BepInDependency.DependencyFlags.HardDependency)] //yes that's the entire guid
    [BepInDependency("imabatby.lethallevelloader", BepInDependency.DependencyFlags.HardDependency)] //using this to get enemy/scrap lists for easy reference because zeekerss is lame :/
    public class TwitchCompany : BaseUnityPlugin
    {

        //Typical mod things
        public static TwitchCompany Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        //fml i should've kept this here
        public static GameObject coolPrefab = new GameObject("TwitchCompanyManager");

        //Config entry things
        public static string[] BALDWhitelistArray;
        public static string[] BALDBlacklistArray;
        public static string[] SupplyDropBlacklistArray;
        public static string[] TreasureDropBlacklistArray;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            ConfigBuilder.InitialiseConfig();
            BALDWhitelistArray = Methods.CSVSeperator(ConfigBuilder.BALDWhitelist.Value);
            BALDBlacklistArray = Methods.CSVSeperator(ConfigBuilder.BALDBlacklist.Value);
            SupplyDropBlacklistArray = Methods.CSVSeperator(ConfigBuilder.SupplyDropBlacklist.Value);
            TreasureDropBlacklistArray = Methods.CSVSeperator(ConfigBuilder.TreasureDropBlacklist.Value);

            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);
            Harmony.PatchAll();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

    }
}
