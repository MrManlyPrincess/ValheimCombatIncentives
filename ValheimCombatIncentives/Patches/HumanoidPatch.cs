using HarmonyLib;
using static ValheimCombatIncentives.ValheimCombatIncentives;

namespace ValheimCombatIncentives.Patches
{
    [HarmonyPatch(typeof(Humanoid))]
    public static class HumanoidPatch
    {
        [HarmonyPatch(nameof(Humanoid.BlockAttack))]
        [HarmonyPrefix]
        public static bool PrefixBlockAttack(HitData hit, Character attacker, Humanoid __instance, ref HitData __state)
        {
            // Clone this and save it in state. The "hit" parameter gets modified during the original method if the attack is parried.
            // We need an unmodified version.
            __state = hit.Clone();
            return true;
        }

        [HarmonyPatch(nameof(Humanoid.BlockAttack))]
        [HarmonyPostfix]
        public static bool PostfixBlockAttack(bool __result, HitData hit, Character attacker, Humanoid __instance,
            HitData __state)
        {
            if (!__result) return false;

            var currentBlocker = __instance.GetCurrentBlocker();

            var didParry = currentBlocker.m_shared.m_timedBlockBonus > 1.0 &&
                           __instance.m_blockTimer > -1.0f && __instance.m_blockTimer < 0.25;

            var skillFactor = __instance.GetSkillFactor(Skills.SkillType.Blocking);
            var blockPower = currentBlocker.GetBlockPower(skillFactor);

            if (didParry)
            {
                blockPower *= currentBlocker.m_shared.m_timedBlockBonus;
            }

            // Apply blocker resistance
            if (currentBlocker.m_shared.m_damageModifiers.Count > 0)
            {
                var modifiers = new HitData.DamageModifiers();

                modifiers.Apply(currentBlocker.m_shared.m_damageModifiers);
                __state.ApplyResistance(modifiers, out _);
            }

            var hitWithoutShield = __state.Clone();
            var hitWithShield = __state.Clone();

            hitWithShield.ApplyArmor(blockPower);

            var damageWithoutShield = hitWithoutShield.GetTotalBlockableDamage();
            var damageWithShield = hitWithShield.GetTotalBlockableDamage();

            var amountMitigated = damageWithoutShield - damageWithShield;

            var experienceBonus = Utils.GetExperienceBonusFromDamage(__instance,
                __state.GetAttacker(),
                amountMitigated,
                BlockDamageExperienceMultiplier.Value);

            if (didParry)
            {
                experienceBonus *= ParryExperienceMultiplier.Value;
            }

            __instance.RaiseSkill(Skills.SkillType.Blocking, experienceBonus);

            Jotunn.Logger.LogInfo($"\nGranted {Skills.SkillType.Blocking} {experienceBonus} experience!\n" +
                                  $"Post-Resistance Damage: {__state.GetTotalBlockableDamage()}\n" +
                                  $"Blocking Device: {currentBlocker.m_shared.m_name} (BP w/ Skill: {blockPower})\n" +
                                  $"Parried: {didParry}\n" +
                                  $"Resulting Damage: {damageWithShield}\n" +
                                  $"Damage Blocked: {amountMitigated} \n" +
                                  $"Experience Bonus: {experienceBonus}");

            Utils.ShowExperienceNotification(Skills.SkillType.Blocking, experienceBonus);
            return true;
        }
    }
}
