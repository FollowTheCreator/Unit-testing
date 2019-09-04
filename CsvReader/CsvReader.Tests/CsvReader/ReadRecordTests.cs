using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CsvReader.Tests.CsvReader
{
    public class ReadRecordTests
    {
        const string CsvPath = @"D:\git tasks\Unit-testing\CsvReader\CsvReader\bin\Debug\netcoreapp2.1\File.csv";

        [Fact]
        public void ReadRecordResultNotNull()
        {
            // Arrange
            var sr = new StreamReader(CsvPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv);

            // Act
            var record = records.ReadRecord();

            // Assert
            foreach (var pair in record)
            {
                Assert.NotNull(pair.Key);
                Assert.NotNull(pair.Value);
            }
        }

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
                Assert.IsType<string>(pair.Key);
                Assert.IsType<string>(pair.Value);
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
        public void UseCsvReaderWithCustomOptionalParameters()
        {
            // Arrange
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(
                @"1;first;1
                2;second;2
                3;;3
                4;"""";4"
            );
            sw.Flush();
            ms.Position = 0;

            var sr = new StreamReader(ms);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv, delimiter: ";", hasHeaders: false);

            // Act
            var record = records.ReadRecord();

            // Assert
            foreach (var pair in record)
            {
                Assert.IsType<string>(pair.Key);
                Assert.IsType<string>(pair.Value);
            }
        }
    }
}
