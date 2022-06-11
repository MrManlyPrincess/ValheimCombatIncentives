namespace ValheimCombatIncentives.Extensions
{
    public static class HitDataExtensions
    {
        public static bool IsFromRangedWeapon(this HitData hit)
        {
            return hit.m_ranged && hit.m_skill == Skills.SkillType.Bows ||
                   hit.m_skill == Skills.SkillType.Spears;
        }
    }
}
