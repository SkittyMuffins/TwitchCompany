using HarmonyLib;

namespace TwitchCompany.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    public class StartOfRoundPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void StartOfRoundPostfix(StartOfRound __instance)
        {
            HUDManager.Instance.DisplayTip("mod works", "yippee", false);
        }
    }
}
