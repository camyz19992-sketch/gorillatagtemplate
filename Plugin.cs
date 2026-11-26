using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace YourMenuName
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Harmony harmony;

        void Awake()
        {
            harmony = new Harmony(PluginInfo.GUID);
            harmony.PatchAll();
            Logger.LogInfo($"{PluginInfo.Name} loaded!");
        }
    }
}