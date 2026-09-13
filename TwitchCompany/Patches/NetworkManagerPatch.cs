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
        private static void SetSingletonPostfix()
        {
            GameObject coolPrefab = new GameObject("TwitchCompanyManager");
            coolPrefab.hideFlags |= HideFlags.HideAndDontSave;
            Object.DontDestroyOnLoad(coolPrefab);
            var netcomponent = coolPrefab.AddComponent<NetworkObject>();
            coolPrefab.AddComponent<TwitchCompanyManager>();
            
            netcomponent.GlobalObjectIdHash = GetHash("TwitchCompanyManager");

            NetworkManager.Singleton.PrefabHandler.AddNetworkPrefab(coolPrefab);
            return;

            static uint GetHash(string value)
            {
                return value?.Aggregate(17u, (current, c) => unchecked((current * 31) ^ c)) ?? 0u;
            }
        }
    }
}
