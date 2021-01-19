using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Common.Security.Criptografy
{
    public class TwoWay
    {
        private const string _fixedTokenPrefix = "32oXMn2mdy8SqU$R";
        private const string _baseToken = "w6ggGRr!8Yf$xnFL";

        private string _token;
        public string Token
        {
            private get { return _fixedTokenPrefix + (_token ?? _baseToken); }
            set { _token = value; }
        }

        private Exception _exception;
        public Exception Exception => _exception;

        public string Crypt(string text, string customToken = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("Text to crypt cant be null or white space!");
        
            if (!string.IsNullOrWhiteSpace(customToken))
                Token = customToken;

            _exception = null;

            var utf8enc = new UTF8Encoding();
            var md5csp = new MD5CryptoServiceProvider();
            var tDesCsp = new TripleDESCryptoServiceProvider
            {
                Key = md5csp.ComputeHash(utf8enc.GetBytes(Token)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cryptoTransform;
            byte[] bsEncrypted;
            byte[] bs2Encrypt = utf8enc.GetBytes(text);

            try
            {
                cryptoTransform = tDesCsp.CreateEncryptor();
                bsEncrypted = cryptoTransform.TransformFinalBlock(bs2Encrypt, 0, bs2Encrypt.Length);
            }
            catch (Exception ex)
            {
                _exception = ex;
                return string.Empty;
            }
            finally
            {
                tDesCsp.Clear();
                md5csp.Clear();
            }

            return Convert.ToBase64String(bsEncrypted);
        }
        public async Task<string> CryptAsync(string text, string personalToken = null) => await Task.FromResult(Crypt(text, personalToken));

        public string Decrypt(string text, string customToken = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("Text to decrypt cant be null or white space!");

            if (!string.IsNullOrWhiteSpace(customToken))
                Token = customToken;

            _exception = null;

            var utf8enc = new UTF8Encoding();
            var md5csp = new MD5CryptoServiceProvider();
            var tDesCsp = new TripleDESCryptoServiceProvider
            {
                Key = md5csp.ComputeHash(utf8enc.GetBytes(Token)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cryptoTransform;
            byte[] bsDecrypted;
            byte[] bs2Decrypt = null;
            try
            {
                bs2Decrypt = Convert.FromBase64String(text);
            }
            catch
            {
                try
                {
                    bs2Decrypt = utf8enc.GetBytes(text);
                }
                catch (Exception ex)
                {
                    _exception = ex;
                    tDesCsp.Clear();
                    md5csp.Clear();
                    return string.Empty;
                }
            }

            try
            {
                cryptoTransform = tDesCsp.CreateDecryptor();
                bsDecrypted = cryptoTransform.TransformFinalBlock(bs2Decrypt, 0, bs2Decrypt.Length);
            }
            catch (Exception ex)
            {
                _exception = ex;
                return string.Empty;
            }
            finally
            {
                tDesCsp.Clear();
                md5csp.Clear();
            }

            return utf8enc.GetString(bsDecrypted);
        }
        public async Task<string> DecryptAsync(string text, string personalToken = null) => await Task.FromResult(Decrypt(text, personalToken));
    }
}
