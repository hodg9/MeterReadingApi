namespace MeterReadingApi.Api.Dtos
{
    public record MeterReadingUploadResult(int TotalRecords, int SuccessfulRecords, int FailedRecords);
}
