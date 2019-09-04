using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CsvReader.Tests
{
    public class CsvReaderTests
    {
        string CsvPath = @"D:\git tasks\Unit-testing\CsvReader\CsvReader\bin\Debug\netcoreapp2.1\File.csv";

        [Fact]
        public void CheckTypeOfReadRecordResult()
        {
            // Arrange
            var sr = new StreamReader(CsvPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv);

            // Act
            var record = records.ReadRecord();

            // Assert
            foreach(var pair in record)
            {
                Assert.NotNull(pair.Key);
                Assert.NotNull(pair.Value);
                Assert.IsType<string>(pair.Key);
                Assert.IsType<string>(pair.Value);
            }
        }

        [Fact]
        public void CheckTypeOfReadValuesResult()
        {
            // Arrange
            var sr = new StreamReader(CsvPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv);

            // Act
            var record = records.ReadValues();

            // Assert
            foreach (var value in record)
            {
                Assert.NotNull(value);
                Assert.IsType<string>(value);
            }
        }

        [Fact]
        public void CallReadRecordMoreTimesThanRecordsCount()
        {
            // Arrange
            var sr = new StreamReader(CsvPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv);
            var recordsCount = records.Count;

            // Act
            for(int i = 0; i < recordsCount - 1; i++)
            {
                records.ReadRecord();
            }
            var lastRecord = records.ReadRecord();
            var afterLastRecord = records.ReadRecord();

            // Assert
            Assert.Equal(lastRecord, afterLastRecord);
        }

        [Fact]
        public void CallReadValuesMoreTimesThanRecordsCount()
        {
            // Arrange
            var sr = new StreamReader(CsvPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv);
            var recordsCount = records.Count;

            // Act
            for (int i = 0; i < recordsCount - 1; i++)
            {
                records.ReadValues();
            }
            var lastRecord = records.ReadValues();
            var afterLastRecord = records.ReadValues();

            // Assert
            Assert.Equal(lastRecord, afterLastRecord);
        }
    }
}
