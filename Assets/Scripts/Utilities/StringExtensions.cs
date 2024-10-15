using System;
using System.IO;
using UnityEngine;

namespace Utilities
{
    public static class StringExtensions
    {
        public static string GetColored(this string str,Color color)
        {
            var hexColor = ColorUtility.ToHtmlStringRGBA(color);
            return $"<color=#{hexColor}>{str}</color>";
        }
        
        
        public static void PrintColored(this string str, Color color)
        {
            str.GetColored(color).Print();
        }
        
        public static void PrintColored(this string str, Color color, GameObject obj)
        {
            str.GetColored(color).Print(obj);
        }
    }
}