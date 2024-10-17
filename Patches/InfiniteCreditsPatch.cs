using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMP_Patch.Patches
{
    [HarmonyPatch(typeof(Terminal), "RunTerminalEvents")]
    class InfiniteCreditsPatch
    {
        [HarmonyPostfix]
        static void Postfix(ref int ___groupCredits)
        {
            ___groupCredits = 69420;
        }
    }
}
