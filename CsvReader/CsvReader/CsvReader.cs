using CsvReader.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvReader
{
    public class CsvReader<T>
    {
        private readonly T[] _records;
        private int _index;

        public CsvReader(StreamReader streamReader, CsvHelper.CsvReader csvReader, string delimiter = ",", bool hasHeaders = true)
        {
            if (streamReader == null)
            {
                throw new ArgumentNullException(nameof(streamReader));
            }
            if (csvReader == null)
            {
                throw new ArgumentNullException(nameof(csvReader));
            }

            streamReader.DiscardBufferedData();
            streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            csvReader.Configuration.HasHeaderRecord = hasHeaders;
            csvReader.Configuration.Delimiter = delimiter;
            if (hasHeaders)
            {
                csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                csvReader.Configuration.RegisterClassMap<RecordCsvMap>();
            }

            _records = csvReader.GetRecords<T>().ToArray();
            _index = -1;
        }

        public T Current
        {
            get
            {
                return _records[_index];
            }
        }

        public int Count
        {
            get
            {
                return _records.Length;
            }
        }

        public Dictionary<string, string> ReadRecord()
        {
            if (_index < _records.Length - 1)
            {
                _index++;
            }

            var recordProperties = typeof(T).GetProperties();
            var result = new Dictionary<string, string>(recordProperties.Length);
            foreach (var prop in recordProperties)
            {
                result.Add($"{prop.Name}", $"{prop.GetValue(Current)}");
            }

            return result;
        }

        public List<string> ReadValues()
        {
            if (_index < _records.Length - 1)
            {
                _index++;
            }

            var properties = typeof(T).GetProperties();
            var result = new List<string>(properties.Length);
            foreach (var prop in properties)
            {
                result.Add(prop.GetValue(Current).ToString());
            }

            return result;
        }
    }
}
