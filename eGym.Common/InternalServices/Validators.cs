using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace eGym.Common.InternalServices
{
    internal sealed class Validators
    {
        #region E-MAIL
        private bool IsInvalid { get; set; }
        private string DomainMapper(Match match)
        {
            var idnMapping = new IdnMapping();
            var domainName = match.Groups[2].Value;
            try
            {
                domainName = idnMapping.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                IsInvalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        /// <summary>
        /// Verify a mail address
        /// </summary>
        public bool MailAddress(string address)
        {
            IsInvalid = false;
            if (string.IsNullOrWhiteSpace(address))
                return false;
            else
            {
                try
                {
                    address = Regex.Replace(address, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }

                if (IsInvalid)
                    return false;
                else
                {
                    try
                    {
                        return Regex.IsMatch(address, @"([a-zA-Z0-9\-\_\.]+)\@([a-zA-Z0-9\-\_\.]{3,})\.([a-zA-Z]{2,4})", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    }
                    catch (RegexMatchTimeoutException)
                    {
                        return false;
                    }
                }
            }
        }
        #endregion
    }
}
