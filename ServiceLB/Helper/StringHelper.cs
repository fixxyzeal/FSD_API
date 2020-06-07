using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLB.Helper
{
    public static class StringHelper
    {
        public static string GetCodePoint(string codePoint)
        {
            int code = (int)new System.ComponentModel.Int32Converter().ConvertFromString(codePoint);
            return char.ConvertFromUtf32(code);
        }
    }
}