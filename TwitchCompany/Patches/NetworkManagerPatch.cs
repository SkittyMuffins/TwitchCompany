using HarmonyLib;
using System.Linq;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;

namespace TwitchCompany.Patches
{
    [HarmonyPatch(typeof(NetworkManager))]
    internal static class NetworkManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(NetworkManager.SetSingleton))]
        private static void RegisterNetworker()
        {
            TwitchCompany.coolPrefab = new GameObject("TwitchCompany Networker");
            TwitchCompany.coolPrefab.hideFlags |= HideFlags.HideAndDontSave;
            Object.DontDestroyOnLoad(TwitchCompany.coolPrefab);
            var netcomponent = TwitchCompany.coolPrefab.AddComponent<NetworkObject>();
            TwitchCompany.coolPrefab.AddComponent<NetworkingStuffs>();

            //janky reflection bullcrap because idk how else to get this to work
            var fieldInfo = typeof(NetworkObject).GetField("GlobalObjectIdHash", BindingFlags.Instance | BindingFlags.Public);
            fieldInfo!.SetValue(netcomponent, GetHash(MyPluginInfo.PLUGIN_GUID));

            NetworkManager.Singleton.PrefabHandler.AddNetworkPrefab(TwitchCompany.coolPrefab);
            return;

            static uint GetHash(string value)
            {
                return value?.Aggregate(17u, (current, c) => unchecked((current * 31) ^ c)) ?? 0u;
            }
        }
    }
}
