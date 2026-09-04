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

        //Raid hordes
        public static ConfigEntry<bool> EnableHordeSpawning { get; set; }
        public static ConfigEntry<int> HordeMaxSize { get; set; }
        public static ConfigEntry<string> HordeEnemyType { get; set; }

        //Raid supply drops
        public static ConfigEntry<bool> EnableSupplyDropSpawning { get; set; }
        public static ConfigEntry<int> SupplyDropMaxSize { get; set; }
        public static ConfigEntry<ItemDropLocations> SupplyDropLocation { get; set; }

        //Raid treasure drops
        public static ConfigEntry<bool> EnableTreasureDropSpawning { get; set; }
        public static ConfigEntry<int> TreasureDropMaxSize { get; set; }
        public static ConfigEntry<ItemDropLocations> TreasureDropLocation { get; set; }

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

            //Horde settings
            EnableHordeSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Horde Spawning",
                true,
                "If enabled, a horde of enemies will spawn when your stream is raided."
                );

            HordeMaxSize = TwitchCompany.Instance.Config.Bind<int>(
                "Raid Events",
                "Horde Max Size",
                10,
                "The maximum number of enemies that can spawn in a horde."
                );

            HordeEnemyType = TwitchCompany.Instance.Config.Bind<string>(
                "Raid Events",
                "Horde Enemy Type",
                "Masked",
                "The type of enemy that will spawn in a horde. Must be a valid enemy type."
                );

            //Supply drop settings
            EnableSupplyDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Supply Drop Spawning",
                true,
                "If enabled, a supply drop of tools will spawn when your stream is raided."
                );

            SupplyDropMaxSize = TwitchCompany.Instance.Config.Bind<int>(
                "Raid Events",
                "Supply Drop Max Size",
                5,
                "The maximum number of tools that can spawn in a supply drop."
                );

            SupplyDropLocation = TwitchCompany.Instance.Config.Bind<ItemDropLocations>(
                "Raid Events",
                "Supply Drop Location",
                ItemDropLocations.InShip,
                "The location where the supply drop will arrive. Can be spawned in the middle of the ship (InShip) or on the host player (OnHost)."
                );

            //Treasure drop settings
            EnableTreasureDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Treasure Drop Spawning",
                true,
                "If enabled, a random selection of scrap will spawn when your stream is raided."
                );

            TreasureDropMaxSize = TwitchCompany.Instance.Config.Bind<int>(
                "Raid Events",
                "Treasure Drop Max Size",
                5,
                "The maximum number of scrap items that can spawn from a treasure drop."
                );

            TreasureDropLocation = TwitchCompany.Instance.Config.Bind<ItemDropLocations>(
                "Raid Events",
                "Treasure Drop Location",
                ItemDropLocations.InShip,
                "The location where the treasure drop will arrive. Can be spawned in the middle of the ship (InShip) or on the host player (OnHost)."
                );
        }

        public enum ItemDropLocations
        {
            InShip,
            OnHost
        }
    }
}
