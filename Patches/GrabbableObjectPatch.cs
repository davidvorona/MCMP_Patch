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
    [HarmonyPatch(typeof(GrabbableObject), "GrabItem")]
    class GrabbableObjectPatch
    {
        [HarmonyPostfix]
        static void Postfix(GrabbableObject __instance)
        {
            float carryWeightValue = (105 * __instance.playerHeldBy.carryWeight) - 105;
            if (carryWeightValue > 100)
            {
                TryUnlockEmoteAchievement(Achievements.CheddahLaden, __instance.playerHeldBy.playerUsername);
            }
        }
    }
}
