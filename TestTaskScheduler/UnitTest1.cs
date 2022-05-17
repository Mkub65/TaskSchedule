using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using TaskScheduler;
using XSystem.Security.Cryptography;

namespace TestTaskScheduler
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void FullProcess()
        {
            var recordList = CsvMethodes.Reader();
            
            var list = TaskCsv.ToList(recordList);
            var startDateList = TaskCsv.FromList(list);
            startDateList.Sort();
            CsvMethodes.Writer(startDateList);

            const string originalFile = @"./Zadania.csv";
            const string copiedFile = @"C:\Users\Maciek\Desktop\Zadania.csv";

            var originalHash = GetFileHash(originalFile);
            var copiedHash = GetFileHash(copiedFile);

            Assert.AreEqual(copiedHash, originalHash);

        }

        public static string GetFileHash(string filename)
        {
            var hash = new SHA1Managed();
            var clearBytes = File.ReadAllBytes(filename);
            var hashedBytes = hash.ComputeHash(clearBytes);
            return ConvertBytesToHex(hashedBytes);
        }

        public static string ConvertBytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x"));
            }
            return sb.ToString();
        }

    }
}