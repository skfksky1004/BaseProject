using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class EnumUtility
    {
        public static void GetFlagsCheck<T>(long flagValue, Action<int> checkAction)
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

        public static long SetFlagsCheck<T>(bool[] arrCheck)
        {
            int retFlag = 0x0;
            int length = Enum.GetValues(typeof(T)).Length;
            for (int i = 0; i < length; i++)
            {
                if (arrCheck[i])
                {
                    //           0000 = 0x0000
                    //    1<<0 = 0001 = 0x0001
                    //    1<<1 = 0010 = 0x0002
                    //    1<<2 = 0100 = 0x0004
                    //    1<<3 = 1000 = 0x0008
                    retFlag |= 1 << (i - 1);
                }
            }

            return retFlag;
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