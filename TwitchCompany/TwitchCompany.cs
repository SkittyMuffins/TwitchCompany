using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using TwitchChatAPI;

namespace TwitchCompany
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("TwitchChatAPI", BepInDependency.DependencyFlags.HardDependency)] //yes that's the entire guid
    public class TwitchCompany : BaseUnityPlugin
    {
        public static TwitchCompany Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        //Config things go here.

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            InitialiseConfigs();

            NetcodePatcher();

            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);
            Harmony.PatchAll();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        private void NetcodePatcher()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }
        }
        private void InitialiseConfigs()
        {
            //Put in config init things here there'll be a lot of those. maybe move into another file in future?
        }

    }
}
