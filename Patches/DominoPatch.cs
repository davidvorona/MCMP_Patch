using GameNetcodeStuff;
using HarmonyLib;
using TooManyEmotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using TooManyEmotes.Networking;
using static MCMP_Patch.HelperTools;
using static MCMP_Patch.CustomLogging;

namespace MCMP_Patch.Patches
{
    [HarmonyPatch(typeof(StartOfRound), "UnlockShipObject")]
    class DominoPatch
    {
        static readonly string emoteID = "dancin'_domino";

        static readonly int inverseTpUnlockableID = 19;

        [HarmonyPostfix]
        static void Postfix(ref int unlockableID)
        {
            if (unlockableID == inverseTpUnlockableID)
            {
                UnlockableEmote dominoEmote = EmotesManager.allUnlockableEmotesDict[emoteID];
                if (!SessionManager.IsEmoteUnlocked(dominoEmote))
                {
                    if (!ConfigSync.instance.syncShareEverything)
                        SessionManager.UnlockEmoteLocal(dominoEmote);

                    SyncManager.SendOnUnlockEmoteUpdate(dominoEmote.emoteId);

                    AddChatMessage("Domino emote unlocked!");
                    Log("Inverse TP purchased: domino emote unlocked.");
                }
            }
        }
    }
}
