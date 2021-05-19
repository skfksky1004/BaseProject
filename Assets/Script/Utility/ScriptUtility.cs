using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class EnumUtility
    {
        public static void CheckFlags<T>(long flagValue, Action<int> checkAction)
        {
            var flags = Enum.GetValues(typeof(T));
            for (int i = 0; i < flags.Length; i++)
            {
                var flag = (T)Enum.Parse(typeof(T), flags.GetValue(i).ToString());
                var flagNum = Convert.ToInt64(flag);
                if (0 == (flagValue & flagNum))
                {
                    checkAction?.Invoke(i);
                }
            }
        }
    }

    public static class StringUtility
    {
        public static string ColorStr(this string str, string hexColor)
        {
            hexColor = hexColor.Replace("#", "");
            return $"<color=#{hexColor}>{str}</color>";
        }

        public static string ColorStr(this string str, Color color)
        {
            var hexColor = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{hexColor}>{str}</color>";
        }
    }
}