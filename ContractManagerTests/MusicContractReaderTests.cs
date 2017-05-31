using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ContractManager;
using NUnit.Framework;

namespace ContractManagerTests
{
    [TestFixture]
    public class MusicContractReaderTests
    {
        [Test]
        public void MustThrowExceptionWhenFilePathIsInvalid()
        {
            Assert.That(() => new ContractsReader<MusicContract>(""), 
                Throws.TypeOf<InvalidFilePathException>().With.Message.Contains("File path cannot be empty or null"));
        }

        [Test]
        public void MustThrowExceptionWhenFileCannotBeFound()
        {
            var filePath = "InvalidFilePath";

            Assert.That(() => new ContractsReader<MusicContract>(filePath),
                Throws.TypeOf<FileNotFoundException>().With.Message.Contains(filePath));
        }

        [Test]
        public void ReadTheFileAndReturnAListOfMusicContracts()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\MusicContracts.txt");

            ContractsReader<MusicContract> reader = new ContractsReader<MusicContract>(filePath);
            IList<MusicContract> contractList = reader.ReadAll().ToList();
            reader.Dispose();

            Assert.AreEqual(7, contractList.Count);
            Assert.AreEqual("Tinie Tempah", contractList[0].Artist);
            Assert.AreEqual("Frisky (Live from SoHo)", contractList[0].Title);
            Assert.AreEqual("digital download", contractList[0].Usages[0]);
            Assert.AreEqual("streaming", contractList[0].Usages[1]);
            Assert.IsNotNull(contractList[0].StartDate);
            Assert.AreEqual(1, ((DateTime)contractList[0].StartDate).Day);
            Assert.AreEqual(02, ((DateTime)contractList[0].StartDate).Month);
            Assert.AreEqual(2012, ((DateTime)contractList[0].StartDate).Year);
            Assert.IsNull(contractList[0].EndDate);
            
            Assert.AreEqual("Monkey Claw", contractList[6].Artist);
            Assert.AreEqual("Christmas Special", contractList[6].Title);
            Assert.AreEqual("streaming", contractList[6].Usages[0]);
            Assert.IsNotNull(contractList[6].StartDate);
            Assert.AreEqual(25, ((DateTime)contractList[6].StartDate).Day);
            Assert.AreEqual(12, ((DateTime)contractList[6].StartDate).Month);
            Assert.AreEqual(2012, ((DateTime)contractList[6].StartDate).Year);
            Assert.IsNotNull(contractList[6].EndDate);
            Assert.AreEqual(31, ((DateTime)contractList[6].EndDate).Day);
            Assert.AreEqual(12, ((DateTime)contractList[6].EndDate).Month);
            Assert.AreEqual(2012, ((DateTime)contractList[6].EndDate).Year);
        }
    }
}
