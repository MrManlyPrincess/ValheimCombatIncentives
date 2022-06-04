using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;
using UnityEngine.Events;
using ValheimWards.Extensions;

namespace ValheimWards
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.NotEnforced, VersionStrictness.Minor)]
    internal class ValheimWards : BaseUnityPlugin
    {
        public const string PluginGUID = "com.mrmanlyprincess.ValheimWards";
        public const string PluginName = "Valheim Wards";
        public const string PluginVersion = "0.0.1";

        private static CustomLocalization ModLocalization = LocalizationManager.Instance.GetLocalization();

        #region Config

        private static ConfigEntry<bool> ModEnabled;

        public static ConfigEntry<float> WardRadius;

        #endregion

        private void Awake()
        {
            CreateConfigValues();
            if (!ModEnabled.Value) return;

            AddLocalizations();

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void Update()
        {
            if (!ModEnabled.Value) return;
            if (!Player.m_localPlayer) return;

            var areaImInside = PrivateArea.m_allAreas.FirstOrDefault(area => area.IsEnabled() && area.IsInside(Player.m_localPlayer.transform.position, 0));
            if (!areaImInside) return;

            Jotunn.Logger.LogInfo($"Area: {areaImInside}");
        }

        #region Setup

        private void CreateConfigValues()
        {
            Config.SaveOnConfigSet = true;
            Config.ConfigReloaded += ReloadConfig;
            Config.SettingChanged += UpdateSettings;

            ModEnabled = Config.Bind("General", "Enabled", true, "Enable this mod");
            WardRadius = Config.Bind("General", "Ward Radius", 10f,
                "Override radius for wards, base game's default is 10.");
        }

        private void ReloadConfig(object sender, EventArgs args)
        {
            Jotunn.Logger.LogInfo($"ReloadConfig => {sender}, {args}");
        }

        private void UpdateSettings(object sender, SettingChangedEventArgs args)
        {
            Jotunn.Logger.LogInfo($"UpdateSettings => {sender}, {args}");
        }

        private void AddLocalizations()
        {
            // ModLocalization.AddTranslation("English", new Dictionary<string, string>
            // {
            //     { "offload_button_text", "Offload" }
            // });
        }

        #endregion
    }
}
