using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TwitchChatAPI;
using TwitchChatAPI.Objects;
using Steamworks.Data;

namespace TwitchCompany
{
    internal class TwitchCompanyManager : MonoBehaviour
    {
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
        }

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
            String[] msg = Methods.BuildBALDContents(message);
            Methods.Tip(msg[0], msg[1], false);
        }
    }
}
