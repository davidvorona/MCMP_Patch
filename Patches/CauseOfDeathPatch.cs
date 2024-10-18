using GameNetcodeStuff;
using HarmonyLib;
using TooManyEmotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooManyEmotes.Networking;
using static MCMP_Patch.HelperTools;
using static MCMP_Patch.CustomLogging;

namespace MCMP_Patch.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
    class CauseOfDeathPatch
    {
        // It is what it is what it is what it is what it is
        static readonly string emoteID1 = "go_with_the_flow";

        static readonly string emoteID2 = "smug_dance";

        [HarmonyPostfix]
        static void Postfix(CauseOfDeath causeOfDeath, PlayerControllerB __instance)
        {
            string playerUsername = __instance.playerUsername;
            if (causeOfDeath == CauseOfDeath.Bludgeoning)
            {
                UnlockableEmote whatItIsEmote = EmotesManager.allUnlockableEmotesDict[emoteID1];
                if (!SessionManager.IsEmoteUnlocked(whatItIsEmote, playerUsername))
                {
                    if (!ConfigSync.instance.syncShareEverything)
                        SessionManager.UnlockEmoteLocal(whatItIsEmote, false, playerUsername);

                    SyncManager.SendOnUnlockEmoteUpdate(whatItIsEmote.emoteId);

                    AddAchievement(Achievements.ItIsWhatItIs, playerUsername);
                    AddChatMessage(whatItIsEmote.displayNameColorCoded + " emote unlocked!");
                }
            }
            if (causeOfDeath == CauseOfDeath.Gravity)
            {
                UnlockableEmote dancinEmote = EmotesManager.allUnlockableEmotesDict[emoteID2];
                if (!SessionManager.IsEmoteUnlocked(dancinEmote, playerUsername))
                {
                    if (!ConfigSync.instance.syncShareEverything)
                        SessionManager.UnlockEmoteLocal(dancinEmote, false, playerUsername);

                    SyncManager.SendOnUnlockEmoteUpdate(dancinEmote.emoteId);

                    AddAchievement(Achievements.FallGuy, playerUsername);
                    AddChatMessage(dancinEmote.displayNameColorCoded + " emote unlocked!");
                }
            }
        }
    }
}
