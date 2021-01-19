using System;
using System.Globalization;

namespace eGym.Common.InternalServices
{
    internal sealed class Formatters : IFormatProvider, ICustomFormatter
    {
        readonly CultureInfo culture;
        const int ACCT_LENGTH = 12;

        string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable) return ((IFormattable)arg).ToString(format, culture);
            else if (arg != null) return arg.ToString();
            else return string.Empty;
        }

        public Formatters()
        {
            culture = CultureInfo.CurrentCulture;
        }

        public Formatters(CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
                cultureInfo = CultureInfo.CurrentCulture;
            culture = cultureInfo;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg.GetType() != typeof(int))
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", format), e);
                }

            string ufmt = format.ToUpper(CultureInfo.InvariantCulture);
            if (!(ufmt == "H" || ufmt == "I"))
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", format), e);
                }

            string result = arg.ToString();

            if (result.Length < ACCT_LENGTH)
                result = result.PadLeft(ACCT_LENGTH, '0');
            if (result.Length > ACCT_LENGTH)
                result = result.Substring(0, ACCT_LENGTH);

            if (ufmt == "I")
                return result;
            else
                return result.Substring(0, 5) + "-" + result.Substring(5, 3) + "-" + result.Substring(8);
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }
    }
}
