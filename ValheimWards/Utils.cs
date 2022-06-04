using System.Collections.Generic;
using UnityEngine;

namespace ValheimWards
{
    public static class Utils
    {
        public static void ShowCenterMessage(string message)
        {
            Player.m_localPlayer.Message(MessageHud.MessageType.Center, message);
        }

        private static int? ParseAndClampInt(string value, int min, int max)
        {
            if (int.TryParse(value, out var outValue)) return Mathf.Clamp(outValue, min, max);
            return null;
        }
    }
}
