using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MCMP_Patch.HelperTools;

namespace MCMP_Patch.Patches
{
    [HarmonyPatch(typeof(Terminal), "ParsePlayerSentence")]
    class TerminalPatch
    {
        static readonly int inverseTpUnlockableID = 19;

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
                PlayerControllerB player = GetPlayerUsingTerminal();
                string playerUsername = player?.playerUsername;
                if (__result.shipUnlockableID == inverseTpUnlockableID)
                {
                    TryUnlockEmoteAchievement(Achievements.DominoEffect, playerUsername);
 
                }
                else if (__result.buyRerouteToMoon != -1 && __result.buyRerouteToMoon != -2 && __result.itemCost > 0)
                {
                    TryUnlockEmoteAchievement(Achievements.SpaceIsTheBest, playerUsername);
                }
            }
        }

        public static PlayerControllerB GetPlayerUsingTerminal()
        {
            foreach (PlayerControllerB player in StartOfRound.Instance.allPlayerScripts)
            {
                if (player.inTerminalMenu)
                {
                    return player;
                }
            }
            return null;
        }
    }
}
