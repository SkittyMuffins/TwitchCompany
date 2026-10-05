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
using UnityEngine.ProBuilder;
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

        public static string[][] SplitIntoTwoStringArrays(string[] array, char delimiter)
        {
            string[] arr1 = new string[array.Length];
            string[] arr2 = new string[array.Length];

            for(int i = 0; i<array.Length; i++)
            {
                string[] parts = array[i].Split(delimiter);
                arr1[i] = parts[0];
                arr2[i] = parts[1];
            }

            string[][] compoundArray = new string[2][];
            compoundArray[0] = arr1;
            compoundArray[1] = arr2;

            return compoundArray;
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

        public static void SummonItemsWithRarityAtLocation(List<SpawnableItemWithRarity> itemPool, int count, Vector3 pos, bool parentToShip, TwitchCompanyManager manager)
        {
            int[] weights = new int[itemPool.Count];
            Item[] items = new Item[itemPool.Count];
            
            //splitting everything for easier iteration + getting total weight in my other method
            for(int i = 0; i<itemPool.Count; i++)
            {
                weights[i] = itemPool[i].rarity;
                items[i] = itemPool[i].spawnableItem;
            }

            //selecting the item via my other method and spawning it
            Item selectedItem = WeightedRandom<Item>(items, weights);

            NetworkObjectReference[] spawnedItems = new NetworkObjectReference[count];
            int[] values = new int[count];

            //TODO make this go backwards
            for(int i = 0; i<count; i++)
            {
                GameObject spawnedItem = GameObject.Instantiate(selectedItem.spawnPrefab, pos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
                GrabbableObject grab = spawnedItem.GetComponentInChildren<GrabbableObject>();
                NetworkObject netObj = spawnedItem.GetComponentInChildren<NetworkObject>();
                grab.scrapValue = (int)(RoundManager.Instance.AnomalyRandom.Next(selectedItem.minValue, selectedItem.maxValue) * RoundManager.Instance.scrapValueMultiplier);

                netObj.Spawn();
                if(parentToShip)
                {
                    spawnedItem.transform.parent = StartOfRound.Instance.shipAnimatorObject.transform;
                }

                spawnedItems[i] = netObj;
                values[i] = grab.scrapValue;
            }

            //i was almost gonna FindObjectOfType this before ctrl+fing the lethal modding discord for it. thanks buttery for happening to talk about how zeeks does this and what he should be doing instead you saved me
            
            RoundManager.Instance.SyncScrapValuesClientRpc(spawnedItems, values);
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
