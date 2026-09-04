using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitchChatAPI;
using TwitchChatAPI.Objects;
using UnityEngine;

namespace TwitchCompany
{
    internal class Methods
    {
        public static void Tip(string title, string message, bool isError)
        {
            try
            {
                HUDManager.Instance.DisplayTip(title, message, isError);
            }
            catch (Exception ex)
            {
                TwitchCompany.Logger.LogError($"Tip failed to display. Probably broken by a mod or base game update. Error is as follows:");
                throw ex;
            }
        }

        public static String[] CSVSeperator(String csv)
        {
            if (string.IsNullOrEmpty(csv))
            {
                return new string[0];
            }
            string[] entries = csv.Split(',');
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i] = entries[i].Trim();
            }
            return entries;
        }

        public static PlayerControllerB getPlayerByID(ulong actualclientid)
        {
            foreach(PlayerControllerB player in StartOfRound.Instance.allPlayerScripts)
            {
                if(actualclientid == player.actualClientId)
                {
                    return player;
                }
            }
            return null;
        }

        public static void SummonItemsWithRarityAtLocation(List<SpawnableItemWithRarity> itemPool, Transform location)
        {

        }
    }
}
