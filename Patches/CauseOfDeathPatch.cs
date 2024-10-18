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
    [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
    class CauseOfDeathPatch
    {
        [HarmonyPostfix]
        static void Postfix(CauseOfDeath causeOfDeath, PlayerControllerB __instance)
        {
            string playerUsername = __instance.playerUsername;
            if (causeOfDeath == CauseOfDeath.Bludgeoning)
            {
                TryUnlockEmoteAchievement(Achievements.ItIsWhatItIs, playerUsername);
            }
            else if (causeOfDeath == CauseOfDeath.Gravity)
            {
                TryUnlockEmoteAchievement(Achievements.FallGuy, playerUsername);
            }
            else if (causeOfDeath == CauseOfDeath.Inertia)
            {
                TryUnlockEmoteAchievement(Achievements.GenerationalCrashout, playerUsername);
            }
        }
    }
}
