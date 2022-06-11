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
using ValheimCombatIncentives.Extensions;

namespace ValheimCombatIncentives
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.NotEnforced, VersionStrictness.Minor)]
    internal class ValheimCombatIncentives : BaseUnityPlugin
    {
        public const string PluginGUID = "com.mrmanlyprincess.ValheimCombatIncentives";
        public const string PluginName = "Valheim Combat Incentives";
        public const string PluginVersion = "0.0.1";

        private static CustomLocalization ModLocalization = LocalizationManager.Instance.GetLocalization();
        private static bool HasEverBeenEnabled;

        #region Config

        public static ConfigEntry<bool> ModEnabled;
        public static ConfigEntry<bool> ShowNotifications;
        public static ConfigEntry<float> NotificationExperienceThreshold;

        public static ConfigEntry<float> DamageExperienceMultiplier;
        public static ConfigEntry<float> BlockDamageExperienceMultiplier;
        public static ConfigEntry<float> SneakDamageExperienceMultiplier;

        public static ConfigEntry<float> ParryExperienceMultiplier;

        public static ConfigEntry<float> AssassinateExperienceMultiplier;

        public static ConfigEntry<float> KnifeSurpriseAttackMultiplier;
        public static ConfigEntry<float> SecondaryAttackMultiplier;

        public static ConfigEntry<float> BowDistanceMultiplier;
        public static ConfigEntry<float> SpearDistanceMultiplier;

        public static ConfigEntry<float> DrawMinMultiplier;
        public static ConfigEntry<float> DrawMaxMultiplier;

        #endregion

        private void Awake()
        {
            CreateConfigValues();

            // TODO: Handle enabled check everywhere else since we can toggle it mid-game now
            if (!ModEnabled.Value) return;
            if (HasEverBeenEnabled) return;

            AddLocalizations();

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            HasEverBeenEnabled = true;
        }

        #region Setup

        private void CreateConfigValues()
        {
            Config.SaveOnConfigSet = true;
            Config.ConfigReloaded += ReloadConfig;
            Config.SettingChanged += UpdateSettings;

            ModEnabled = Config.Bind("General", "Enabled", true, "Enable this mod");
            ShowNotifications = Config.Bind("General", "Show Notifications", true, "Show notifications on XP events");
            NotificationExperienceThreshold = Config.Bind("General", "Notification Experience Threshold",
                0f,
                "The threshold after which to show a notification for an experience bonus.");

            #region Damage Multipliers

            DamageExperienceMultiplier = Config.Bind("Damage Multipliers", "Dealt Damage Multiplier",
                0.05f,
                "The multiplier to apply to damage dealt to determine the amount of bonus experience given.");

            BlockDamageExperienceMultiplier = Config.Bind("Damage Multipliers", "Blocked Damage Multiplier",
                0.1f,
                "The multiplier to apply to damage blocked to determine the amount of bonus experience given.");

            SneakDamageExperienceMultiplier = Config.Bind("Damage Multipliers", "Sneak Damage Experience Multiplier",
                0.3f,
                "The multiplier to apply to surprise attack damage to determine the amount of sneak experience to be given.");

            #endregion

            #region Skill Multipliers

            ParryExperienceMultiplier = Config.Bind("Skill Multipliers", "Parry Bonus Experience Multiplier",
                1.2f,
                $"The multiplier to apply AFTER the '{BlockDamageExperienceMultiplier.Definition.Key}', only in the case that a parry was achieved.");

            AssassinateExperienceMultiplier = Config.Bind("Skill Multipliers",
                "Assassination Sneak Experience Multiplier", 2.0f,
                $"The multiplier to apply AFTER the '{SneakDamageExperienceMultiplier.Definition.Key}', only in the case that the surprise attack killed the target.");

            #endregion

            #region Incentive Multipliers

            var drawMinKey = "Bow Draw Percentage Multiplier - Minimum";
            var drawMaxKey = "Bow Draw Percentage Multiplier - Maximum";

            KnifeSurpriseAttackMultiplier = Config.Bind("Incentive Multipliers", "Knife Surprise Attack Multiplier",
                1.5f,
                $"The multiplier to apply AFTER the '{DamageExperienceMultiplier.Definition.Key}' when surprise attacks are performed ");

            SecondaryAttackMultiplier = Config.Bind("Incentive Multipliers", "Secondary Attack Multiplier",
                1.1f,
                $"The multiplier to apply AFTER the '{DamageExperienceMultiplier.Definition.Key}' when secondary attacks are performed ");

            BowDistanceMultiplier = Config.Bind("Incentive Multipliers", "Bow Distance Multiplier",
                0.1f,
                $"The multiplier to apply AFTER the '{DamageExperienceMultiplier.Definition.Key}' when bow attacks are performed." +
                "This is first applied to the distance that the ranged attack was made from, then the result is applied to the existing experienceBonus.");

            SpearDistanceMultiplier = Config.Bind("Incentive Multipliers", "Spear Distance Multiplier",
                0.2f,
                $"The multiplier to apply AFTER the '{DamageExperienceMultiplier.Definition.Key}' when spear throws are performed." +
                "This is first applied to the distance that the ranged attack was made from, then the result is applied to the existing experienceBonus.");

            DrawMinMultiplier = Config.Bind("Incentive Multipliers", drawMinKey,
                0.4f,
                $"The minimum multiplier to apply AFTER the '{DamageExperienceMultiplier.Definition.Key}' and the '{BowDistanceMultiplier.Definition.Key}' when bow attacks are performed." +
                $"This is used (along with {drawMaxKey}) to determine how to adjust the experience bonus based on how much the bow was drawn.");

            DrawMaxMultiplier = Config.Bind("Incentive Multipliers", drawMaxKey,
                1.1f,
                $"The maximum multiplier to apply AFTER the '{DamageExperienceMultiplier.Definition.Key}' and the '{BowDistanceMultiplier.Definition.Key}' when bow attacks are performed." +
                $"This is used (along with {drawMinKey}) to determine how to adjust the experience bonus based on how much the bow was drawn.");

            #endregion
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
