﻿using System.IO;
using Xunit;

namespace CsvReader.Tests.CsvReader
{
    public class ReadValuesTests
    {
        const string DefaultCsvPath = "DefaultCsv.csv";
        const string CsvForCustomParametersPath = "CsvForCustomParameters.csv";

        [Fact]
        public void ReadValuesResultNotNull()
        {
            // Arrange
            var sr = new StreamReader(DefaultCsvPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv);

            // Act
            var record = records.ReadValues();

            // Assert
            foreach (var value in record)
            {
                Assert.NotNull(value);
            }
        }

        [Fact]
        public void CheckTypeOfReadValuesResult()
        {
            // Arrange
            var sr = new StreamReader(DefaultCsvPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv);

            // Act
            var record = records.ReadValues();

            // Assert
            foreach (var value in record)
            {
                Assert.IsType<string>(value);
            }
        }

        [Fact]
        public void CallReadValuesMoreTimesThanRecordsCount()
        {
            // Arrange
            var sr = new StreamReader(DefaultCsvPath);
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

        [Fact]
        public void UseCsvReaderWithCustomOptionalParameters()
        {
            // Arrange
            var sr = new StreamReader(CsvForCustomParametersPath);
            var csv = new CsvHelper.CsvReader(sr);

            var records = new CsvReader<Models.Record>(sr, csv, delimiter: ";", hasHeaders: false);

            // Act
            var record = records.ReadValues();

            // Assert
            foreach (var value in record)
            {
                Assert.IsType<string>(value);
            }
        }
    }
}
