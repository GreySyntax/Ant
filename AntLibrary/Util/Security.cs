using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AntLibrary.Util
{
    class Security
    {
        public static string CalculateSHA512(string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            SHA512CryptoServiceProvider cryptoTransformSHA1 =
            new SHA512CryptoServiceProvider();
            string hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }
    }
}
