using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLB.Helper
{
    public static class StringHelper
    {
        public static string GetCodePoint(string codePoint)
        {
            int code = int.Parse(codePoint, System.Globalization.NumberStyles.HexNumber);
            string unicodeString = char.ConvertFromUtf32(code);
            return unicodeString;
        }
    }
}