using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TwitchChatAPI;
using TwitchChatAPI.Objects;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.PlayerLoop;
using LethalLevelLoader;

namespace TwitchCompany
{
    internal class TwitchCompanyManager : NetworkBehaviour
    {
        public static TwitchCompanyManager Instance { get; private set; }
        private static Queue<(string enemyType, int count, ulong playerID)> spawnQueue = new Queue<(string enemyType, int count, ulong playerID)>();
        private static bool canSpawn = false;

        private void Awake()
        {
            if(ConfigBuilder.EnableChatEvents.Value)
            {
                API.OnMessage += OnMessageHandler;
            }
            if(ConfigBuilder.EnableRaidEvents.Value)
            {
                API.OnRaid += OnRaidHandler;
            }
        }

        public override void OnNetworkSpawn()
        {
            Instance = this;
            base.OnNetworkSpawn();
            if (IsServer) return;
        }

        /// <summary>
        /// Event responses
        /// </summary>
        private static void OnMessageHandler(TwitchMessage message)
        {
            TwitchCompany.Logger.LogInfo($"Received message from {message.User.DisplayName}: {message.Message}");

            //check for BALD chats
            if(ConfigBuilder.EnableBALD.Value)
            {
                EvaluateForBALD(message);
            }
        }

        private static void OnRaidHandler(TwitchRaidEvent raid)
        {
            TwitchCompany.Logger.LogInfo($"Received raid from {raid.User.DisplayName} with {raid.ViewerCount} viewers.");

            //send raid alert if enabled
            if(ConfigBuilder.EnableRaidAlerts.Value)
            {
                Methods.Tip("INCOMING RAID", $"{raid.User.DisplayName} is raiding with {raid.ViewerCount} viewers!!!", true);
            }

            //spawn horde if enabled
            if(ConfigBuilder.EnableHordeSpawning.Value)
            {
                QueueEnemySpawnOnPlayer(ConfigBuilder.HordeEnemyType.Value,
                    Math.Min(raid.ViewerCount, ConfigBuilder.HordeMaxSize.Value),
                    StartOfRound.Instance.localPlayerController.playerClientId); //i really hope this gets the host otherwise we may be fucked
            }

            //handle supply drop spawning if enabled
            if(ConfigBuilder.EnableSupplyDropSpawning.Value)
            {

            }

            //handle treasure drop spawning if enabled
            if(ConfigBuilder.EnableTreasureDropSpawning.Value)
            {

            }
        }

        /// <summary>
        /// Unity methods such as FixedUpdate to handle spawning of enemies on players etc.
        /// </summary>
        private void FixedUpdate()
        {
            //Lock/unlock the spawn queue depending on if in orbit or not
            if(StartOfRound.Instance is not null &&
                (!StartOfRound.Instance.inShipPhase || StartOfRound.Instance.shipHasLanded))
            {
                canSpawn = true;
            }
            else canSpawn = false;

            if(IsServer && canSpawn && spawnQueue.Count > 0)
            {
                //insert spawn queue method here
                //is now a bad time to say i lifted a lot of this code from nightofthelivingmimic
                //because i lifted a lot of this code from nightofthelivingmimic
                //i realise that mod doesn't have a license but that's a problem for future me
            }
        }


        /// <summary>
        /// Helper methods that're too specific to be in the Methods class
        /// </summary>
        private static void EvaluateForBALD(TwitchMessage message)
        {
            if(message.Message.StartsWith(ConfigBuilder.BALDPrefix.Value))
            {
                //oh god i have to check so many things from config
                if(!ConfigBuilder.ModsCanBALD.Value
                    && !ConfigBuilder.SubscribersCanBALD.Value
                    && !ConfigBuilder.VIPSCanBALD.Value
                    && !ConfigBuilder.EnableBALDBlacklist.Value
                    && !ConfigBuilder.EnableBALDWhitelist.Value)
                {
                    SendBALD(message);
                    return;
                }
                else
                {
                    //this is some ugly ass code but idk how to make it any better
                    if(ConfigBuilder.EnableBALDWhitelist.Value)
                    {
                        if(Array.Exists(TwitchCompany.BALDWhitelistArray, username => username.Equals(message.User.Username, StringComparison.OrdinalIgnoreCase)))
                        {
                            SendBALD(message);
                            return;
                        }
                    }

                    if(ConfigBuilder.EnableBALDBlacklist.Value)
                    {
                        if(!Array.Exists(TwitchCompany.BALDBlacklistArray, username => username.Equals(message.User.Username, StringComparison.OrdinalIgnoreCase)))
                        {
                            SendBALD(message);
                            return;
                        }
                    }
                    else if(ConfigBuilder.ModsCanBALD.Value && message.User.IsModerator)
                    {
                        SendBALD(message);
                        return;
                    }
                    else if(ConfigBuilder.SubscribersCanBALD.Value && message.User.IsSubscriber)
                    {
                        SendBALD(message);
                        return;
                    }
                    else if(ConfigBuilder.VIPSCanBALD.Value && message.User.IsVIP)
                    {
                        SendBALD(message);
                        return;
                    }
                }
            }
        }

        private static void SendBALD(TwitchMessage message)
        {
            //because i'm not typing this out 73895734895 times fuck that noise
            String[] msg = BuildBALDContents(message);
            Methods.Tip(msg[0], msg[1], false);
        }

        public static String[] BuildBALDContents(TwitchMessage message)
        {
            string hostname;
            try
            {
                hostname = StartOfRound.Instance?.localPlayerController.playerUsername;
            }
            catch (Exception ex)
            {
                TwitchCompany.Logger.LogError($"Failed to get hostname: {ex.Message}");
                hostname = "Player";
            }

            string messageContent = message.Message.Substring(ConfigBuilder.BALDPrefix.Value.Length).Trim();

            string header = $"{hostname}'s chat";
            string body = $"{message.User.DisplayName}: {messageContent}";
            return new string[] { header, body };
        }

        [ServerRpc(RequireOwnership = false)]
        //Queues the spawning of enemies on players to prevent spawns from occurring in orbit
        public static void QueueEnemySpawnOnPlayer(string enemyType, int count, ulong playerID)
        {
            spawnQueue.Enqueue((enemyType, count, playerID));
        }

        private static void ProcessSpawnQueue()
        {
            while (spawnQueue.Count > 0)
            {
                var (enemyType, count, playerID) = spawnQueue.Dequeue();
                ExtendedEnemyType type = PatchedContent.ExtendedEnemyTypes.Find(enemy => string.Equals(enemy.EnemyType.enemyName, enemyType, StringComparison.Ordinal));
                //UNFINISHED. Get player by their ID here
                if (type != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        //Actually spawn the enemy
                    }
                }
                else
                {
                    TwitchCompany.Logger.LogError($"Enemy type '{enemyType}' not found in ExtendedEnemyTypes.");
                }
            }
        }
    }
}
