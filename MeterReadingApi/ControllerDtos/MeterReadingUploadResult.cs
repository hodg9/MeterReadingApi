namespace MeterReadingApi.Dtos
{
    public class MeterReadingUploadResult
    {
        public int TotalRecords { get; init; }
        public int SuccessfulRecords { get; init; }
        public int FailedRecords { get; init; }
    }
}
