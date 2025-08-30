using CsvHelper;
using CsvHelper.Configuration;
using MeterReadingApi.Domain;
using System.Globalization;

namespace MeterReadingApi.Services
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

    public record MeterReadingCsvParserResult(IReadOnlyList<MeterReadingCsvRecord> Records, IReadOnlyList<string> BadRows);

    public record MeterReadingCsvRecord(int AccountId, DateTime MeterReadingDateTime, int MeterReadValue);
}
