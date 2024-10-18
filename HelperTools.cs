using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMP_Patch
{
    internal static class HelperTools
    {
        public enum Achievements
        {
            DominoEffect,
            SpaceIsTheBest,
            ItIsWhatItIs,
            FallGuy
        }

        public static PlayerControllerB localPlayerController { get { return StartOfRound.Instance?.localPlayerController; } }

        public static void AddChatMessage(string text)
        {
            HUDManager.Instance.AddTextToChatOnServer(text);
        }

        public static void AddAchievement(Achievements achievement, string playerUsername = null)
        {
            string username = playerUsername ?? localPlayerController.playerUsername;
            string text = $"<color=yellow>{username} unlocked an achievement:</color> ";
            if (achievement == Achievements.DominoEffect)
            {
                AddChatMessage(text + "Domino Effect");
            }
            else if (achievement == Achievements.SpaceIsTheBest)
            {
                AddChatMessage(text + "Space Is the Best");
            }
            else if (achievement == Achievements.ItIsWhatItIs)
            {
                AddChatMessage(text + "It Is What It Is");
            }
            else if (achievement == Achievements.FallGuy)
            {
                AddChatMessage(text + "Fall Guy");
            }
        }
    }
}
