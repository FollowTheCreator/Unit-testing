using System.IO;
using Xunit;

namespace CsvReader.Tests.CsvReader
{
    public class ReadRecordTests
    {
        const string DefaultCsvPath = "DefaultCsv.csv";
        const string CsvForCustomParametersPath = "CsvForCustomParameters.csv";

        [Fact]
        public void ReadRecordResultNotNull()
        {
            // Arrange
            var sr = new StreamReader(DefaultCsvPath);
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
            var sr = new StreamReader(DefaultCsvPath);
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
            var sr = new StreamReader(DefaultCsvPath);
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
            var sr = new StreamReader(CsvForCustomParametersPath);
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
