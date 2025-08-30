namespace MeterReadingApi.Domain
{
    public interface IMeterReadingRepository
    {
        MeterReading? GetReading(int accountId, DateTime readingDateTime);

        void AddRange(IEnumerable<MeterReading> readings);

    }

}