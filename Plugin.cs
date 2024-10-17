using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using MCMP_Patch.Patches;
using System;
using Unity;
using UnityEngine;
using static MCMP_Patch.CustomLogging;

namespace MCMP_Patch
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static Plugin instance;

        public static ManualLogSource defaultLogger { get { return instance.Logger; } }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            InitLogger();

            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(DominoPatch));
            // DEBUG
            // harmony.PatchAll(typeof(InfiniteCreditsPatch));

            Log(PluginInfo.PLUGIN_GUID + " has loaded successfully.");
        }
    }
}
