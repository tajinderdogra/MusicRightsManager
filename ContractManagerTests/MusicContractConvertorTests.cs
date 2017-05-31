using System;
using System.Collections.Generic;
using ContractManager;
using NUnit.Framework;

namespace ContractManagerTests
{
    [TestFixture]
    public class MusicContractConvertorTests
    {
        [Test]
        public void ReturnNullWhenDictionaryObjectIsEmpty()
        {
            IDictionary<string, string> dataRow = new Dictionary<string, string>();
            MusicContractConvertor convertor = new MusicContractConvertor();
            
            var musicContract = convertor.ConvertToObject(dataRow);
            Assert.IsNull(musicContract);
        }

        [Test]
        public void ReturnNullWhenDictionaryObjectIsNull()
        {
            MusicContractConvertor convertor = new MusicContractConvertor();

            var musicContract = convertor.ConvertToObject(null);
            Assert.IsNull(musicContract);
        }

        [Test]
        public void CheckThatFieldsAreEmptyWhenInputIsEmpty()
        {
            IDictionary<string, string> dataRow = new Dictionary<string, string>();
            dataRow["Artist"] = "";
            dataRow["Title"] = "";
            dataRow["Usages"] = "";

            MusicContractConvertor convertor = new MusicContractConvertor();
            var contract = convertor.ConvertToObject(dataRow);

            Assert.IsTrue(string.IsNullOrEmpty(contract.Artist));
            Assert.IsTrue(string.IsNullOrEmpty(contract.Title));
            Assert.IsEmpty(contract.Usages);
        }

        [Test]
        public void CheckThatNonDateFieldsArePopulatedCorrectlyWhenInputHasData()
        {
            IDictionary<string, string> dataRow = new Dictionary<string, string>();
            dataRow["Artist"] = "Tinie Tempah";
            dataRow["Title"] = "Frisky(Live from SoHo)";
            dataRow["Usages"] = "digital download, streaming";
            
            MusicContractConvertor convertor = new MusicContractConvertor();
            var contract = convertor.ConvertToObject(dataRow);

            Assert.AreEqual(dataRow["Artist"], contract.Artist);
            Assert.AreEqual(dataRow["Title"], contract.Title);
            Assert.AreEqual("digital download", contract.Usages[0]);
            Assert.AreEqual("streaming", contract.Usages[1]);
        }

        [Test]
        public void CheckIfStartDateAndEndDateAreNullWhenInputIsBlank()
        {
            IDictionary<string, string> dataRow = new Dictionary<string, string>();
            dataRow["StartDate"] = "";
            dataRow["EndDate"] = "";

            MusicContractConvertor convertor = new MusicContractConvertor();
            var contract = convertor.ConvertToObject(dataRow);

            Assert.IsNull(contract.StartDate);
            Assert.IsNull(contract.EndDate);
        }

        [Test]
        public void CheckIfStartDateAndEndDateIsParsedCorrectlyWhenInputHasSpecialFormatAndShortMonthName()
        {
            IDictionary<string, string> dataRow = new Dictionary<string, string>();
            dataRow["StartDate"] = "1st Feb 2012";
            dataRow["EndDate"] = "1st Feb 2012";

            MusicContractConvertor convertor = new MusicContractConvertor();
            var contract = convertor.ConvertToObject(dataRow);
            
            Assert.IsNotNull(contract.StartDate);
            Assert.IsNotNull(contract.EndDate);
            Assert.AreEqual(1, ((DateTime)contract.StartDate).Day);
            Assert.AreEqual(2, ((DateTime)contract.StartDate).Month);
            Assert.AreEqual(2012, ((DateTime)contract.StartDate).Year);
            Assert.AreEqual(1, ((DateTime)contract.EndDate).Day);
            Assert.AreEqual(2, ((DateTime)contract.EndDate).Month);
            Assert.AreEqual(2012, ((DateTime)contract.EndDate).Year);
        }

        [Test]
        public void CheckIfStartDateAndEndDateIsParsedCorrectlyWhenInputHasSpecialFormatAndFullMonthName()
        {
            IDictionary<string, string> dataRow = new Dictionary<string, string>();
            dataRow["StartDate"] = "1st August 2012";
            dataRow["EndDate"] = "1st August 2012";

            MusicContractConvertor convertor = new MusicContractConvertor();
            var contract = convertor.ConvertToObject(dataRow);

            Assert.IsNotNull(contract.StartDate);
            Assert.IsNotNull(contract.EndDate);
            Assert.AreEqual(1, ((DateTime)contract.StartDate).Day);
            Assert.AreEqual(8, ((DateTime)contract.StartDate).Month);
            Assert.AreEqual(2012, ((DateTime)contract.StartDate).Year);
            Assert.AreEqual(1, ((DateTime)contract.EndDate).Day);
            Assert.AreEqual(8, ((DateTime)contract.EndDate).Month);
            Assert.AreEqual(2012, ((DateTime)contract.EndDate).Year);
        }

        [Test]
        public void CheckIfStartDateAndEndDateIsParsedCorrectlyWhenInputHasSpecialFormatAndInvalidDayPattern()
        {
            IDictionary<string, string> dataRow = new Dictionary<string, string>();
            dataRow["StartDate"] = "29st August 2012";
            dataRow["EndDate"] = "29st August 2012";

            MusicContractConvertor convertor = new MusicContractConvertor();
            var contract = convertor.ConvertToObject(dataRow);

            Assert.IsNotNull(contract.StartDate);
            Assert.IsNotNull(contract.EndDate);
            Assert.AreEqual(29, ((DateTime)contract.StartDate).Day);
            Assert.AreEqual(8, ((DateTime)contract.StartDate).Month);
            Assert.AreEqual(2012, ((DateTime)contract.StartDate).Year);
            Assert.AreEqual(29, ((DateTime)contract.EndDate).Day);
            Assert.AreEqual(8, ((DateTime)contract.EndDate).Month);
            Assert.AreEqual(2012, ((DateTime)contract.EndDate).Year);
        }
    }
}
