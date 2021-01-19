using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace eGym.Common.Security.Criptografy
{
    public class OneWay
    {
        public static string MD5Hash(string text)
        {
            StringBuilder sb = new StringBuilder();
            MD5CryptoServiceProvider md5cs = new MD5CryptoServiceProvider();
            byte[] bs = md5cs.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string SHA1Hash(string text)
        {
            StringBuilder sb = new StringBuilder();
            SHA1CryptoServiceProvider sha1cs = new SHA1CryptoServiceProvider();
            byte[] bs = sha1cs.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte b in bs)
            {
                sb.Append(b.ToString());
            }
            return sb.ToString();
        }
    }
}
