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

        //Raid alerts
        public static ConfigEntry<bool> EnableRaidAlerts { get; set; }
        public static ConfigEntry<bool> RaidAlertsAreErrors { get; set; }

        //Hordes, supply drops
        public static ConfigEntry<bool> EnableRaidHordeSpawning { get; set; }
        public static ConfigEntry<bool> EnableRaidSupplyDropSpawning { get; set; }
        public static ConfigEntry<bool> EnableRaidTreasureDropSpawning { get; set; }

        /// <summary>
        /// Config entries for things that require cheer handling
        /// </summary>
        public static ConfigEntry<bool> EnableCheerEvents { get; set; }
        public static ConfigEntry<int> MinimumBitAmount { get; set; }

        //Cheer alerts
        public static ConfigEntry<bool> EnableCheerAlerts { get; set; }
        public static ConfigEntry<bool> CheerAlertsAreErrors { get; set; }

        //Hordes, supply drops
        public static ConfigEntry<bool> EnableCheerHordeSpawning { get; set; }
        public static ConfigEntry<int> BitsPerEnemy { get; set; }
        public static ConfigEntry<bool> EnableCheerSupplyDropSpawning { get; set; }
        public static ConfigEntry<bool> EnableCheerTreasureDropSpawning { get; set; }
        public static ConfigEntry<int> BitsPerItem { get; set; }

        /// <summary>
        /// Config entries for things that require sub handling
        /// </summary>
        public static ConfigEntry<bool> EnableSubscriptionEvents { get; set; }

        //Sub alerts
        public static ConfigEntry<bool> EnableSubAlerts { get; set; }
        public static ConfigEntry<bool> SubAlertsAreErrors { get; set; }

        //Hordes, supply drops
        public static ConfigEntry<bool> EnableSubHordeSpawning { get; set; }
        public static ConfigEntry<int> EnemiesPerSub { get; set; }
        public static ConfigEntry<bool> EnableSubSupplyDropSpawning { get; set; }
        public static ConfigEntry<bool> EnableSubTreasureDropSpawning { get; set; }
        public static ConfigEntry<int> ItemsPerSub { get; set; }

        /// <summary>
        /// Config entries for horde spawning
        /// </summary>
        public static ConfigEntry<int> HordeMaxSize { get; set; }
        public static ConfigEntry<string> HordeEnemyType { get; set; }

        /// <summary>
        /// Config entries for supply drops
        /// </summary>
        public static ConfigEntry<int> SupplyDropMaxSize { get; set; }
        public static ConfigEntry<ItemDropLocations> SupplyDropLocation { get; set; }
        public static ConfigEntry<string> SupplyDropBlacklist { get; set; }

        /// <summary>
        /// Config entries for treasure drops
        /// </summary>
        public static ConfigEntry<int> TreasureDropMaxSize { get; set; }
        public static ConfigEntry<ItemDropLocations> TreasureDropLocation { get; set; }
        public static ConfigEntry<string> TreasureDropBlacklist { get; set; }


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

            RaidAlertsAreErrors = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Raid Alerts are Errors",
                true,
                "If true, raid alerts will come in with the red warning pop up rather than the yellow one. True by default because I think for raids it's cooler that way"
                );

            EnableRaidHordeSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Horde Spawning for Raids",
                true,
                "If enabled, a horde of enemies will spawn when your stream is raided."
                );

            EnableRaidSupplyDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Supply Drop Spawning for Raids",
                true,
                "If enabled, a supply drop of tools will spawn when your stream is raided."
                );

            EnableRaidTreasureDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Raid Events",
                "Enable Treasure Drop Spawning for Raids",
                true,
                "If enabled, a random selection of scrap will spawn when your stream is raided."
                );

            //Cheer events
            EnableCheerEvents = TwitchCompany.Instance.Config.Bind<bool>(
                "Cheer Events",
                "Enable Cheer Events",
                true,
                "If enabled, TwitchCompany will listen for when chatters cheer with bits."
                );

            MinimumBitAmount = TwitchCompany.Instance.Config.Bind<int>(
                "Cheer Events",
                "Minimum Bit Amount",
                1,
                "The minimum amount of bits required for a bit-related event to fire. Probably increase this if you're a bigger streamer."
                );

            EnableCheerAlerts = TwitchCompany.Instance.Config.Bind<bool>(
                "Cheer Events",
                "Enable Cheer Alerts",
                true,
                "If enabled, an alert will be displayed onscreen when someone cheers bits."
                );

            CheerAlertsAreErrors = TwitchCompany.Instance.Config.Bind<bool>(
                "Cheer Events",
                "Cheer Alerts are Errors",
                false,
                "If true, cheer alerts will come in with the red warning pop up rather than the yellow one."
                );

            EnableCheerHordeSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Cheer Events",
                "Enable Horde Spawning for Cheering",
                false,
                "If enabled, a horde of enemies will spawn on you when someone cheers with bits."
                );

            BitsPerEnemy = TwitchCompany.Instance.Config.Bind<int>(
                "Cheer Events",
                "Bits per Enemy",
                1,
                "Determines the number of bits required to spawn one enemy in a horde when cheering."
                );

            EnableCheerSupplyDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Cheer Events",
                "Enable Supply Drop Spawning for Cheering",
                true,
                "If enabled, a supply drop of tools will spawn when someone cheers."
                );

            EnableCheerTreasureDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Cheer Events",
                "Enable Treasure Drop Spawning for Cheering",
                true,
                "If enabled, a random selection of scrap will spawn when someone cheers."
                );

            BitsPerItem = TwitchCompany.Instance.Config.Bind<int>(
                "Cheer Events",
                "Bits per Item",
                1,
                "Determines the number of bits required to spawn one tool or scrap in a supply/treasure drop."
                );

            //Subscription events
            EnableSubscriptionEvents = TwitchCompany.Instance.Config.Bind<bool>(
                "Subscription Events",
                "Enable Subscription events",
                true,
                "If enabled, TwitchCompany will listen for when people subscribe to your channel."
                );

            EnableSubAlerts = TwitchCompany.Instance.Config.Bind<bool>(
                "Subscription Events",
                "Enable Subscription Alerts",
                true,
                "If enabled, an alert will be displayed onscreen when someone subscribes or gifts subs."
                );

            SubAlertsAreErrors = TwitchCompany.Instance.Config.Bind<bool>(
                "Subscription Events",
                "Subscription Alerts are Errors",
                false,
                "If true, subscription alerts will come in with the red warning pop up rather than the yellow one."
                );

            EnableSubHordeSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Subscription Events",
                "Enable Horde Spawning for Subs",
                true,
                "If enabled, a horde of enemies will spawn when someone subscribes to or gifts subs for your channel."
                );

            EnemiesPerSub = TwitchCompany.Instance.Config.Bind<int>(
                "Subscription Events",
                "Enemies Per Subscription",
                5,
                "The number of enemies spawned for every subscription."
                );

            EnableSubSupplyDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Subscription Events",
                "Enable Supply Drop Spawning for Subs",
                false,
                "If enabled, a supply drop of tools will spawn when someone subscribes to or gifts subs for your channel."
                );

            EnableSubTreasureDropSpawning = TwitchCompany.Instance.Config.Bind<bool>(
                "Subscription Events",
                "Enable Treasure Drop Spawning for Subs",
                false,
                "If enabled, a random selection of scrap will spawn when someone subscribes to or gifts subs for your channel."
                );

            ItemsPerSub = TwitchCompany.Instance.Config.Bind<int>(
                "Subscription Events",
                "Items Per Subscription",
                5,
                "The number of items spawned by supply/treasure drops for every subscription."
                );

            //Horde settings
            HordeMaxSize = TwitchCompany.Instance.Config.Bind<int>(
                "Horde Settings",
                "Horde Max Size",
                10,
                "The maximum number of enemies that can spawn in a horde."
                );

            HordeEnemyType = TwitchCompany.Instance.Config.Bind<string>(
                "Horde Settings",
                "Horde Enemy Type",
                "Masked",
                "The type of enemy that will spawn in a horde. Must be a valid enemy type."
                );

            //Supply drop settings
            SupplyDropMaxSize = TwitchCompany.Instance.Config.Bind<int>(
                "Supply Drops",
                "Supply Drop Max Size",
                10,
                "The maximum number of tools that can spawn in a supply drop."
                );

            SupplyDropLocation = TwitchCompany.Instance.Config.Bind<ItemDropLocations>(
                "Supply Drops",
                "Supply Drop Location",
                ItemDropLocations.InShip,
                "The location where the supply drop will arrive. Can be spawned in the middle of the ship (InShip) or on the host player (OnHost)."
                );

            SupplyDropBlacklist = TwitchCompany.Instance.Config.Bind<string>(
                "Supply Drops",
                "Supply Drop Blacklist",
                "Mapper,Binoculars",
                "A comma-seperated list of items that can't spawn from supply drops."
                );

            //Treasure drop settings
            TreasureDropMaxSize = TwitchCompany.Instance.Config.Bind<int>(
                "Treasure Drops",
                "Treasure Drop Max Size",
                10,
                "The maximum number of scrap items that can spawn from a treasure drop."
                );

            TreasureDropLocation = TwitchCompany.Instance.Config.Bind<ItemDropLocations>(
                "Treasure Drops",
                "Treasure Drop Location",
                ItemDropLocations.InShip,
                "The location where the treasure drop will arrive. Can be spawned in the middle of the ship (InShip) or on the host player (OnHost)."
                );

            TreasureDropBlacklist = TwitchCompany.Instance.Config.Bind<string>(
                "Treasure Drops",
                "Treasure Drop Blacklist",
                "Mapper,Binoculars",
                "A comma-seperated list of items that can't spawn from treasure drops."
                );
        }

        public enum ItemDropLocations
        {
            InShip,
            OnHost
        }
    }
}
