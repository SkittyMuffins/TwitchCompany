using System;
using System.Collections.Generic;
using System.Text;
using TwitchChatAPI;
using TwitchChatAPI.Objects;

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
    }
}
