using HarmonyLib;
using System.Linq;
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
            
            netcomponent.GlobalObjectIdHash = GetHash("TwitchCompany Networker");

            NetworkManager.Singleton.PrefabHandler.AddNetworkPrefab(TwitchCompany.coolPrefab);
            return;

            static uint GetHash(string value)
            {
                return value?.Aggregate(17u, (current, c) => unchecked((current * 31) ^ c)) ?? 0u;
            }
        }
    }
}
