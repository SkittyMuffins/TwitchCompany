using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TwitchChatAPI;
using TwitchChatAPI.Objects;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

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

        public static void SummonItemsWithRarityAtLocation(List<SpawnableItemWithRarity> itemPool, int count, Vector3 pos, bool parentToShip)
        {
            GameObject? ship = null;
            if(parentToShip)
            {
                ship = GameObject.Find("HangarShip"); //this should get the ship if nothing else has named their gameobject that
            }

            int[] weights = new int[itemPool.Count];
            Item[] items = new Item[itemPool.Count];
            SpawnableItemWithRarity[] itemPoolArray = itemPool.ToArray();
            
            //splitting everything into arrays for easier iteration + getting total weight in my other method
            for(int i = 0; i<itemPoolArray.Length; i++)
            {
                weights[i] = itemPoolArray[i].rarity;
                items[i] = itemPoolArray[i].spawnableItem;
            }

            //selecting the item via my other method and spawning it
            Item selectedItem = WeightedRandom<Item>(items, weights);
            for(int i = 0; i<count; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                GameObject spawnedItem = GameObject.Instantiate(selectedItem.spawnPrefab, pos, rotation);
                NetworkObject netObj = spawnedItem.GetComponentInChildren<NetworkObject>();
                netObj.Spawn();
                if(ship is not null)
                {
                    //PLEASEEEE tell me this works
                    spawnedItem.transform.parent = ship.transform;
                }
            }
        }

        public static T WeightedRandom<T>(T[] items, int[] weights)
        {
            //assuming both arrays are equal - idk if i should do this? lmao
            int totalWeight = 0;

            foreach(int weight in weights)
            {
                totalWeight = totalWeight + weight;
            }

            int selectedWeight = Random.RandomRangeInt(0, totalWeight+1);
            int iteratedWeight = 0;

            for(int i = 0; i<items.Length; i++)
            {
                iteratedWeight = iteratedWeight + weights[i];
                if(iteratedWeight >= selectedWeight)
                {
                    return items[i];
                }
            }
            return items[items.Length - 1]; //return the last item as a failsafe
        }
    }
}
