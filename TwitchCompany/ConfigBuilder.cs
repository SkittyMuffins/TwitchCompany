using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchCompany
{
    internal class ConfigBuilder
    {
        /// <summary>
        /// Config entries for things that require chat handling
        /// </summary>
        public static ConfigEntry<bool> EnableChatEvents { get; set; }

        //BALD chat
        public static ConfigEntry<bool> EnableBALD { get; set; }
        public static ConfigEntry<string> BALDPrefix { get; set; }
        public static ConfigEntry<bool> EnableBALDWhitelist { get; set; }
        public static ConfigEntry<string> BALDWhitelist { get; set; }
        public static ConfigEntry<bool> EnableBALDBlacklist { get; set; }
        public static ConfigEntry<string> BALDBlacklist { get; set; }
        public static ConfigEntry<bool> VIPSCanBALD { get; set; }
        public static ConfigEntry<bool> SubscribersCanBALD { get; set; }
        public static ConfigEntry<bool> ModsCanBALD { get; set; }

        /// <summary>
        /// Config entries for things that require raid handling
        /// </summary>
        public static ConfigEntry<bool> EnableRaidEvents { get; set; }
        public static ConfigEntry<bool> EnableRaidAlerts { get; set; }

        public static void InitialiseConfig()
        {
            //Chat events
            EnableChatEvents = TwitchCompany.Instance.Config.Bind<bool>(
                "Chat Events",
                "Enable Chat Events",
                true,
                "If enabled, TwitchCompany will listen for chat messages. Required for BALD chat."
                );

            //BALD chat
            EnableBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "Chat Events",
                "Enable BALD Chat",
                false,
                "If enabled, Twitch chat messages will be displayed ingame if preceded by the specified prefix."
                );

            BALDPrefix = TwitchCompany.Instance.Config.Bind<string>(
                "Chat Events",
                "BALD Chat Prefix",
                "BALD ",
                "The prefix required for a chatter to use when they want their message to be displayed ingame."
                );

            //BALD chat whitelist and blacklist
            EnableBALDWhitelist = TwitchCompany.Instance.Config.Bind<bool>(
                "Chat Events",
                "Enable BALD Chat Whitelist",
                false,
                "If enabled, only chatters on the whitelist will be able to use BALD chat."
                );

            BALDWhitelist = TwitchCompany.Instance.Config.Bind<string>(
                "Chat Events",
                "BALD Chat Whitelist",
                "",
                "A comma-separated list of usernames that are allowed to use BALD chat. Only used if the whitelist is enabled."
                );

            EnableBALDBlacklist = TwitchCompany.Instance.Config.Bind<bool>(
                "Chat Events",
                "Enable BALD Chat Blacklist",
                false,
                "If enabled, chatters on the blacklist will not be able to use BALD chat."
                );

            BALDBlacklist = TwitchCompany.Instance.Config.Bind<string>(
                "Chat Events",
                "BALD Chat Blacklist",
                "",
                "A comma-separated list of usernames that are not allowed to use BALD chat. Only used if the blacklist is enabled."
                );

            //BALD chat role permissions
            VIPSCanBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "Chat Events",
                "VIPS Can Use BALD Chat",
                false,
                "If enabled, Twitch VIPs will be able to use BALD chat. Enabling this or any other of the role permissions will disallow chatters with no role from using it."
                );

            SubscribersCanBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "Chat Events",
                "Subscribers Can Use BALD Chat",
                false,
                "If enabled, Twitch subscribers will be able to use BALD chat. Enabling this or any other of the role permissions will disallow chatters with no role from using it."
                );

            ModsCanBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "Chat Events",
                "Mods Can Use BALD Chat",
                false,
                "If enabled, Twitch mods will be able to use BALD chat. Enabling this or any other of the role permissions will disallow chatters with no role from using it."
                );


            //Raid events
            EnableRaidEvents = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Raid Events",
                true,
                "If enabled, TwitchCompany will listen for raid events. Required for alerts and horde spawning."
                );

            EnableRaidAlerts = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Raid Alerts",
                true,
                "If enabled, an alert will be displayed onscreen when your stream is raided."
                );
        }
    }
}
