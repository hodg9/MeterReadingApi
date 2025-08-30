namespace MeterReadingApi.Application.Dtos
{
    public record MeterReadingCsvRecord(int AccountId, DateTime MeterReadingDateTime, int MeterReadValue);
}
