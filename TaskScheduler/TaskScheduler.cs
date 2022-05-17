namespace TaskScheduler
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;

    internal static class TaskScheduler
    {
        public static void Main(string[] args)
        {
            var listCsv = CsvMethodes.Reader();

            var list = TaskCsv.ToList(listCsv);

            var startDateList = TaskCsv.FromList(list);

            startDateList.Sort();

            CsvMethodes.Writer(startDateList);
        }
    }
}