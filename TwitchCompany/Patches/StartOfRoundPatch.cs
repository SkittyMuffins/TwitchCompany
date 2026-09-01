using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace TwitchCompany.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    public class StartOfRoundPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void StartOfRoundPostfix(StartOfRound __instance)
        {
            if(NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                TwitchCompany.Logger.LogInfo("Trying to instantiate and spawn TwitchCompany manager...");

                GameObject obj = GameObject.Instantiate(TwitchCompany.coolPrefab, Vector3.zero, Quaternion.identity);
                obj.GetComponent<NetworkObject>().Spawn(true);
                TwitchCompany.Logger.LogInfo("Successfully spawned TwitchCompany manager!");
            }
        }
    }
}
