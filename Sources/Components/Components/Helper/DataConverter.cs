namespace PDFiumDotNET.Components.Helper
{
    using System;

    /// <summary>
    /// Class implements various conversion methods.
    /// </summary>
    internal static class DataConverter
    {
        /// <summary>
        /// Converts date time string in ASN.1 format to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="asn1DateTime">String form of date time in ASN.1 format.</param>
        /// <returns>Converted date time. <see cref="DateTime.MinValue"/> in any kind of error.</returns>
        /// <remarks>
        /// Format of date time is: D:YYYYMMDDHHmmSSOHH'mm'. O is Z, - or +.
        /// Part D: is optional.
        /// </remarks>
        public static DateTimeOffset Asn1DateTimeToToDateTime(string asn1DateTime)
        {
            var retValue = DateTimeOffset.MinValue;

            if (string.IsNullOrEmpty(asn1DateTime)
                || (asn1DateTime.Length != 21 && asn1DateTime.Length != 23))
            {
                return retValue;
            }

            // We have: D:YYYYMMDDHHmmSSOHH'mm' or YYYYMMDDHHmmSSOHH'mm'
            if (asn1DateTime.Length == 23
                && asn1DateTime[0] == 'D'
                && asn1DateTime[1] == ':')
            {
                // We have: D:YYYYMMDDHHmmSSOHH'mm'
                // Remove 'D:' at the front
                asn1DateTime = asn1DateTime.Remove(0, 2);
            }

            if (asn1DateTime.Length == 21
                && asn1DateTime[20] == '\'')
            {
                // We have: YYYYMMDDHHmmSSOHH'mm'
                // Remove ''' at the end
                asn1DateTime = asn1DateTime.Remove(asn1DateTime.Length - 1, 1);

                // We have: YYYYMMDDHHmmSSOHH'mm
                // Replace ''' with ':'
                asn1DateTime = asn1DateTime.Replace('\'', ':');

                // We have: YYYYMMDDHHmmSSOHH:mm
                string dateTimeString
                    = asn1DateTime.Substring(0, 4)
                    + "-"
                    + asn1DateTime.Substring(4, 2)
                    + "-"
                    + asn1DateTime.Substring(6, 2)
                    + "T"
                    + asn1DateTime.Substring(8, 2)
                    + ":"
                    + asn1DateTime.Substring(10, 2)
                    + ":"
                    + asn1DateTime.Substring(12, 2)
                    + asn1DateTime.Substring(14);

                // We have: YYYY-MM-DDTHH:mm:SSOHH:mm
                if (dateTimeString[dateTimeString.Length - 6] != '+' && dateTimeString[dateTimeString.Length - 6] != '-')
                {
                    // We have: YYYY-MM-DDTHH:mm:SSZHH:mm
                    dateTimeString = dateTimeString.Substring(0, dateTimeString.Length - 6);

                    // We have: YYYY-MM-DDTHH:mm:SS
                    if (DateTimeOffset.TryParse(dateTimeString, out DateTimeOffset utc))
                    {
                        retValue = new DateTimeOffset(new DateTime(utc.Ticks), TimeSpan.Zero);
                    }
                }
                else
                {
                    // We have: YYYY-MM-DDTHH:mm:SS+HH:mm, or YYYY-MM-DDTHH:mm:SS-HH:mm.
                    if (!DateTimeOffset.TryParse(dateTimeString, out retValue))
                    {
                        retValue = DateTime.MinValue;
                    }
                }
            }

            return retValue;
        }
    }
}
