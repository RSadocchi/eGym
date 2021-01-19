using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace eGym.Common.Security.FileProtector
{
    public class FileProtector
    {

        private Exception _exception;
        public Exception Exception => _exception;
        
        public OperationResults Protect(string text, ContentFile contentFile, KeyFile keyFile)
        {
            _exception = null;
            FileStream stream = null;
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    throw new Exception("Invalid text to protect");

                if (string.IsNullOrWhiteSpace(contentFile.FileName) || string.IsNullOrWhiteSpace(contentFile.FilePath))
                    throw new Exception("Invalid ContentFile");

                if (string.IsNullOrWhiteSpace(keyFile.FileName) || string.IsNullOrWhiteSpace(keyFile.FilePath))
                    throw new Exception("Invalid KeyFile");

                try
                {
                    stream = File.Create(contentFile.ToString());
                }
                catch (Exception ex)
                {
                    _exception = ex;
                    return OperationResults.Error;
                }
                var tDesCsp = new TripleDESCryptoServiceProvider();
                var cs = new CryptoStream(stream, tDesCsp.CreateEncryptor(), CryptoStreamMode.Write);
                var sw = new StreamWriter(cs);
                sw.WriteLine(text);
                sw.Flush();
                sw.Close();

                stream = null;
                try
                {
                    stream = File.Create(keyFile.ToString());
                }
                catch (Exception ex)
                {
                    _exception = ex;
                    return OperationResults.Error;
                }
                var bw = new BinaryWriter(stream);
                bw.Write(tDesCsp.Key);
                bw.Write(tDesCsp.IV);
                bw.Flush();
                bw.Close();

                tDesCsp.Clear();

                return OperationResults.Protected;
            }
            catch (Exception ex)
            {
                _exception = ex;
                return OperationResults.Error;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
        public async Task<OperationResults> ProtectAsync(string text, ContentFile contentFile, KeyFile keyFile)
            => await Task.FromResult(Protect(text: text, contentFile: contentFile, keyFile: keyFile));

        public string Unprotect(ContentFile contentFile, KeyFile keyFile)
        {
            _exception = null;
            FileStream stream = null;
            try
            {
                if (string.IsNullOrWhiteSpace(contentFile.FileName) || string.IsNullOrWhiteSpace(contentFile.FilePath))
                    throw new Exception("Invalid ContentFile");

                if (string.IsNullOrWhiteSpace(keyFile.FileName) || string.IsNullOrWhiteSpace(keyFile.FilePath))
                    throw new Exception("Invalid KeyFile");

                try
                {
                    stream = File.OpenRead(keyFile.ToString());
                }
                catch (Exception ex)
                {
                    _exception = ex;
                    return string.Empty;
                }
                var tDesCsp = new TripleDESCryptoServiceProvider();
                var br = new BinaryReader(stream);
                tDesCsp.Key = br.ReadBytes(24);
                tDesCsp.IV = br.ReadBytes(8);

                stream = null;
                try
                {
                    stream = File.OpenRead(contentFile.ToString());
                }
                catch (Exception ex)
                {
                    _exception = ex;
                    return string.Empty;
                }
                var cs = new CryptoStream(stream, tDesCsp.CreateDecryptor(), CryptoStreamMode.Read);
                var sr = new StreamReader(cs);

                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                _exception = ex;
                return string.Empty;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
        public async Task<string> UnprotectAsync(ContentFile contentFile, KeyFile keyFile)
            => await Task.FromResult(Unprotect(contentFile: contentFile, keyFile: keyFile));
    }
}
