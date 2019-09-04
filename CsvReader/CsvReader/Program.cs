using CsvReader.Models;
using System;
using System.Collections.Generic;
using System.IO;

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
    }
}
