using HarmonyLib;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace TwitchCompany.Patches
{
    [HarmonyPatch(typeof(NetworkManager))]
    internal static class NetworkManagerPatch
    {

        //thanks crit you're a lifesaver
        [HarmonyPostfix]
        [HarmonyPatch(nameof(NetworkManager.SetSingleton))]
        private static void SetSingletonPostfix()
        {
            TwitchCompany.coolPrefab = CreateManagerPrefab();

            NetworkManager.Singleton.AddNetworkPrefab(TwitchCompany.coolPrefab);
        }

        private static GameObject CreateManagerPrefab()
        {
            var prefabHolder = new GameObject("InactiveHolder")
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            prefabHolder.SetActive(false);

            var prefab = new GameObject("TwitchCompanyManager");
            prefab.transform.SetParent(prefabHolder.transform);

            var networkObject = prefab.AddComponent<NetworkObject>();
            networkObject.SynchronizeTransform = false;
            networkObject.AutoObjectParentSync = false;
            networkObject.GlobalObjectIdHash = GetHash("TwitchCompanyManager");

            static uint GetHash(string value)
            {
                return value?.Aggregate(17u, (current, c) => unchecked((current * 31) ^ c)) ?? 0u;
            }

            prefab.AddComponent<TwitchCompanyManager>();

            return prefab;
        }
    }
}
