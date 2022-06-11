using StackingNotifications;
using UnityEngine;
using static ValheimCombatIncentives.ValheimCombatIncentives;

namespace ValheimCombatIncentives
{
    public static class Utils
    {
        public static void ShowExperienceNotification(Skills.SkillType skill, float experience)
        {
            if (!ShowNotifications.Value || experience < NotificationExperienceThreshold.Value) return;
            var color = experience > 50 ? nameof(Color.magenta) :
                experience > 25 ? nameof(Color.red) :
                experience > 10 ? nameof(Color.yellow) :
                nameof(Color.white);

            NotificationHandler.Instance.AddNotification($"{skill} <color={color}>+{experience}</color> XP");
        }

        public static float GetExperienceBonusFromDamage(Character victim, HitData hit, float experienceModifier)
        {
            return GetExperienceBonusFromDamage(victim, hit.GetAttacker(), hit.GetTotalDamage(), experienceModifier);
        }

        public static float GetExperienceBonusFromDamage(Character victim, Character attacker, float damage,
            float experienceModifier)
        {
            Jotunn.Logger.LogDebug(
                "Calculating experience bonus for damage\n" +
                $"Attacker: {attacker}\n" +
                $"Victim: {victim.m_name}\n" +
                $"Total Damage: {damage}");

            var significantDamage = Mathf.Min(damage, victim.GetMaxHealth());
            return Mathf.Max(1, significantDamage * experienceModifier);
        }


        public static float MapToRange(float value, float inputMinimum, float inputMaximum, float outputMinimum,
            float outputMaximum, bool clamp = true)
        {
            if (clamp)
            {
                value = Mathf.Max(inputMinimum, Mathf.Min(value, inputMaximum));
            }

            return (value - inputMinimum) * (outputMaximum - outputMinimum) / (inputMaximum - inputMinimum) +
                   outputMinimum;
        }
    }
}
