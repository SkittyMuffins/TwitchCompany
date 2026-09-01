using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchCompany
{
    internal class ConfigBuilder
    {
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

        public static void InitialiseConfig()
        {
            //BALD chat
            EnableBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "BALD Chat",
                "Enable BALD Chat",
                false,
                "If enabled, Twitch chat messages will be displayed ingame if preceded by the specified prefix."
                );

            BALDPrefix = TwitchCompany.Instance.Config.Bind<string>(
                "BALD Chat",
                "BALD Chat Prefix",
                "BALD ",
                "The prefix required for a chatter to use when they want their message to be displayed ingame."
                );

            //BALD chat whitelist and blacklist
            EnableBALDWhitelist = TwitchCompany.Instance.Config.Bind<bool>(
                "BALD Chat",
                "Enable BALD Chat Whitelist",
                false,
                "If enabled, only chatters on the whitelist will be able to use BALD chat."
                );

            BALDWhitelist = TwitchCompany.Instance.Config.Bind<string>(
                "BALD Chat",
                "BALD Chat Whitelist",
                "",
                "A comma-separated list of usernames that are allowed to use BALD chat. Only used if the whitelist is enabled."
                );

            EnableBALDBlacklist = TwitchCompany.Instance.Config.Bind<bool>(
                "BALD Chat",
                "Enable BALD Chat Blacklist",
                false,
                "If enabled, chatters on the blacklist will not be able to use BALD chat."
                );

            BALDBlacklist = TwitchCompany.Instance.Config.Bind<string>(
                "BALD Chat",
                "BALD Chat Blacklist",
                "",
                "A comma-separated list of usernames that are not allowed to use BALD chat. Only used if the blacklist is enabled."
                );

            //BALD chat role permissions
            VIPSCanBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "BALD Chat",
                "VIPS Can Use BALD Chat",
                true,
                "If enabled, Twitch VIPs will be able to use BALD chat. Enabling this or any other of the role permissions will disallow chatters with no role from using it."
                );

            SubscribersCanBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "BALD Chat",
                "Subscribers Can Use BALD Chat",
                true,
                "If enabled, Twitch subscribers will be able to use BALD chat. Enabling this or any other of the role permissions will disallow chatters with no role from using it."
                );

            ModsCanBALD = TwitchCompany.Instance.Config.Bind<bool>(
                "BALD Chat",
                "Mods Can Use BALD Chat",
                true,
                "If enabled, Twitch mods will be able to use BALD chat. Enabling this or any other of the role permissions will disallow chatters with no role from using it."
                );
        }
    }
}
