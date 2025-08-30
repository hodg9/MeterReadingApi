namespace MeterReadingApi.Application.Dtos
{
    public record MeterReadingCsvParserResult(IReadOnlyList<MeterReadingCsvRecord> Records, IReadOnlyList<string> BadRows);
}
