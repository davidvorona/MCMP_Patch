using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MCMP_Patch.HelperTools;
using TooManyEmotes.Networking;
using TooManyEmotes;

namespace MCMP_Patch.Patches
{
    [HarmonyPatch(typeof(Terminal), "ParsePlayerSentence")]
    class TerminalPatch
    {
        static readonly string emoteID1 = "planetary_vibes";

        [HarmonyPostfix]
        static void Postfix(ref TerminalNode __result, Terminal __instance)
        {
            if (__instance.screenText.text.Length <= 0)
                return;

            string input = __instance.screenText.text.Substring(__instance.screenText.text.Length - __instance.textAdded).ToLower();
            string[] args = input.Split(' ');

            if (args.Length == 0)
                return;

            if ("confirm".StartsWith(input))
            {
                if (__result.buyRerouteToMoon != -1 && __result.buyRerouteToMoon != -2 && __result.itemCost > 0)
                {
                    // TODO: Is there a better way to get the current player using the terminal?
                    foreach (PlayerControllerB player in StartOfRound.Instance.allPlayerScripts)
                    {
                        if (player.inTerminalMenu)
                        {
                            string playerUsername = player.playerUsername;
                            UnlockableEmote planetaryEmote = EmotesManager.allUnlockableEmotesDict[emoteID1];
                            if (!SessionManager.IsEmoteUnlocked(planetaryEmote, playerUsername))
                            {
                                if (!ConfigSync.instance.syncShareEverything)
                                    SessionManager.UnlockEmoteLocal(planetaryEmote, false, playerUsername);

                                SyncManager.SendOnUnlockEmoteUpdate(planetaryEmote.emoteId);

                                AddAchievement(Achievements.SpaceIsTheBest, playerUsername);
                                AddChatMessage(planetaryEmote.displayNameColorCoded + " emote unlocked!");
                            }
                        }
                    }

                }
            }
        }
    }
}
