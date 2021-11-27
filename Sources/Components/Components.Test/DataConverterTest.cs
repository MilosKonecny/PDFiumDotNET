namespace PDFiumDotNET.Components.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Components.Helper;

    // Disable "Remove the underscores from member name"
#pragma warning disable CA1707

    /// <summary>
    /// Unit test class for <see cref="DataConverter"/>.
    /// </summary>
    [TestClass]
    public class DataConverterTest
    {
        /// <summary>
        /// Test for constructor.
        /// </summary>
        [TestMethod]
        public void DataConverter_Asn1DateTimeToToDateTime_Convert_MinValue()
        {
            var dateTime = DataConverter.Asn1DateTimeToToDateTime(null);
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime(string.Empty);
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime("1");
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime("123456789012345678901234");
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime("12345678901234567890123");
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime("123456789012345678901");
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime("D2345678901234567890123");
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime("D:345678901234567890123");
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
            dateTime = DataConverter.Asn1DateTimeToToDateTime("D:34567890123456789012'");
            Assert.AreEqual(DateTimeOffset.MinValue, dateTime);
        }

        /// <summary>
        /// Test for constructor.
        /// </summary>
        [TestMethod]
        public void DataConverter_Asn1DateTimeToToDateTime_Convert_Converted()
        {
            var dateTime = DataConverter.Asn1DateTimeToToDateTime("D:20211214090807+01'00'");
            var universalDateTime = dateTime.ToUniversalTime();
            var expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 8, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("D:20211214090807+02'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 7, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("D:20211214090807-01'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 10, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("D:20211214090807-02'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 11, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("D:20211214090807Z00'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 9, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("20211214090807+01'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 8, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("20211214090807+02'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 7, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("20211214090807-01'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 10, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("20211214090807-02'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 11, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);

            dateTime = DataConverter.Asn1DateTimeToToDateTime("20211214090807Z00'00'");
            universalDateTime = dateTime.ToUniversalTime();
            expectedDateTimeInUtc = new DateTimeOffset(new DateTime(2021, 12, 14, 9, 8, 7), TimeSpan.Zero);
            Assert.AreEqual(expectedDateTimeInUtc, universalDateTime);
        }
    }
}
