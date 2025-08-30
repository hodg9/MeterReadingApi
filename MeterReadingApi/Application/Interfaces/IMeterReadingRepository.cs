using MeterReadingApi.Domain;

namespace MeterReadingApi.Application.Interfaces
{
    public interface IMeterReadingRepository
    {
        MeterReading? GetReading(int accountId, DateTime readingDateTime);

        void AddRange(IEnumerable<MeterReading> readings);

    }

}