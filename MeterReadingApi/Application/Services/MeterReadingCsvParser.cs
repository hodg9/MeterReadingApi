using CsvHelper;
using CsvHelper.Configuration;
using MeterReadingApi.Application.Dtos;
using System.Globalization;

namespace MeterReadingApi.Application.Services
{
    public class MeterReadingCsvParser
    {
        public async Task<MeterReadingCsvParserResult> ParseCsvAsync(Stream csvStream)
        {
            var badRows = new List<string>();
            using var reader = new StreamReader(csvStream);
            using var csv = new CsvReader(reader, new CsvConfiguration(new CultureInfo("en-GB"))
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                BadDataFound = context => { badRows.Add(context.RawRecord); }
            });

            var records = new List<MeterReadingCsvRecord>();
            await foreach (var record in csv.GetRecordsAsync<MeterReadingCsvRecord>())
            {
                records.Add(record);
            }
            return new MeterReadingCsvParserResult(records, badRows);
        }
    }
}
