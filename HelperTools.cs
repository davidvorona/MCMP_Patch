using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooManyEmotes.Networking;
using TooManyEmotes;
using UnityEngine;
using static MCMP_Patch.CustomLogging;

namespace MCMP_Patch
{
    internal static class HelperTools
    {
        // TODO:Add achievements for emotes...
        // hit_it
        // jiggle_jiggle
        public enum Achievements
        {
            DominoEffect,
            SpaceIsTheBest,
            ItIsWhatItIs,
            FallGuy,
            GenerationalCrashout
        }

        public static PlayerControllerB localPlayerController { get { return StartOfRound.Instance?.localPlayerController; } }

        public static void TryUnlockEmoteAchievement(Achievements achievement, string playerUsername = null)
        {
            string username = playerUsername ?? localPlayerController.playerUsername;
            GetAchievementData(achievement, out string emoteId, out string achievementName);
            if (emoteId == null)
            {
                LogWarning("Invalid emote ID for achievement " + achievement.ToString());
                return;
            }
            UnlockableEmote emote = EmotesManager.allUnlockableEmotesDict[emoteId];
            if (!SessionManager.IsEmoteUnlocked(emote, username))
            {
                if (!ConfigSync.instance.syncShareEverything)
                    SessionManager.UnlockEmoteLocal(emote, false, username);

                SyncManager.SendOnUnlockEmoteUpdate(emote.emoteId);

                AddAchievement(achievementName, username);
                AddChatMessage(emote.displayNameColorCoded + " emote unlocked!");
            }
        }

        private static void GetAchievementData(Achievements achievement, out string emoteId, out string achievementName)
        {
            switch (achievement)
            {
                case Achievements.DominoEffect:
                    emoteId = "dancin'_domino";
                    achievementName = "Domino Effect";
                    break;
                case Achievements.SpaceIsTheBest:
                    emoteId =  "planetary_vibes";
                    achievementName = "Space Is the Best";
                    break;
                case Achievements.ItIsWhatItIs:
                    emoteId = "go_with_the_flow";
                    achievementName = "It Is What It Is";
                    break;
                case Achievements.FallGuy:
                    emoteId = "smug_dance";
                    achievementName = "Fall Guy";
                    break;
                case Achievements.GenerationalCrashout:
                    emoteId = "get_down";
                    achievementName = "Generational Crashout";
                    break;
                default:
                    emoteId = null;
                    achievementName = null;
                    break;
            }
        }

        private static void AddChatMessage(string text)
        {
            HUDManager.Instance.AddTextToChatOnServer(text);
        }

        private static void AddAchievement(string achievementName, string playerUsername)
        {
            string text = $"<color=yellow>{playerUsername} unlocked an achievement:</color> ";
            AddChatMessage(text + achievementName);
        }
    }
}
