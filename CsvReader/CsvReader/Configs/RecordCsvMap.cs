using CsvHelper.Configuration;
using CsvReader.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvReader.Configs
{
    public class RecordCsvMap : ClassMap<Record>
    {
        public RecordCsvMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.Name).Name("name");
            Map(m => m.Age).Name("age");
        }
    }
}
