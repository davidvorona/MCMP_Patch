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
    class GrabbableObjectGrabItemPatch
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

    [HarmonyPatch(typeof(GrabbableObject), "OnBroughtToShip")]
    class GrabbableObjectBroughtToShipPatch
    {
        static readonly string cashRegisterItemName = "Cash register";

        [HarmonyPostfix]
        static void Postfix(GrabbableObject __instance)
        {
            if (__instance.playerHeldBy != null && __instance.itemProperties.itemName == cashRegisterItemName)
            {
                TryUnlockEmoteAchievement(Achievements.JiggleJiggle, __instance.playerHeldBy.playerUsername);
            }
        }
    }
}
