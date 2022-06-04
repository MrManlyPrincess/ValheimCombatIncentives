using HarmonyLib;

namespace ValheimWards.Patches
{
    [HarmonyPatch(typeof(PrivateArea))]
    public static class PrivateAreaPatch
    {
        [HarmonyPatch(nameof(PrivateArea.Awake))]
        [HarmonyPrefix]
        static void Awake(PrivateArea __instance)
        {
            __instance.m_radius = ValheimWards.WardRadius.Value;
        }
    }
}
