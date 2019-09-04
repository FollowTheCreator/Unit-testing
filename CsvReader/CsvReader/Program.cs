using CsvReader.Configs;
using CsvReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvReader
{
    class Program
    {
        public const string CsvPath = "File.csv";

        static void Main(string[] args)
        {
            using (var sr = new StreamReader(CsvPath))
            {
                using (var csv = new CsvHelper.CsvReader(sr))
                {
                    var records = new CsvReader<Record>(sr, csv, hasHeaders: false);

                    for(int i = 0; i < records.Count; i++)
                    {
                        var record = records.ReadRecord();
                        WriteDictionary<string, string>(record);
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void WriteDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary != null)
            {
                foreach (var item in dictionary)
                {
                    Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
                }
            }
            else
            {
                Console.WriteLine("End of file.");
            }
        }

        private static void WriteList<T>(List<T> list)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"Value: {item}");
                }
            }
            else
            {
                Console.WriteLine("End of file.");
            }
        }

        //public static Dictionary<string, string> GetRecord(string delimiter = ",", bool hasHeaders = true)
        //{
        //    using (var reader = new StreamReader(CsvPath))
        //    {
        //        using (var csv = new CsvHelper.CsvReader(reader))
        //        {
        //            csv.Configuration.HasHeaderRecord = hasHeaders;
        //            csv.Configuration.Delimiter = delimiter;
        //            csv.Configuration.RegisterClassMap<RecordCsvMap>();

        //            var records = csv.GetRecords<Record>().ToList();

        //            var recordProperties = typeof(Record).GetProperties();
        //            var result = new List<Dictionary<string, string>>(records.Count);
        //            foreach (var record in records)
        //            {
        //                var dictionary = new Dictionary<string, string>(recordProperties.Length);
        //                foreach(var prop in recordProperties)
        //                {
        //                    dictionary.Add($"{prop.Name}", $"{prop.GetValue(record)}");
        //                }
        //                result.Add(dictionary);
        //            }

        //            return result;
        //        }
        //    }
        //}
    }
}
