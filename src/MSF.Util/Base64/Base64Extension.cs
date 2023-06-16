using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Base64
{
    public static class Base64Extension
    {
        public static string TransformTextToBase64(this string text)
        {
            var textoAsBytes = Encoding.UTF8.GetBytes(text);
            var textoAsBase64 = Convert.ToBase64String(textoAsBytes);
            return textoAsBase64;
        }

        public static string TransformBase64ToText(this string base64)
        {
            var base64AsByte = Convert.FromBase64String(base64);
            var base64AsString = Encoding.UTF8.GetString(base64AsByte);
            return base64AsString;
        }
    }
}
