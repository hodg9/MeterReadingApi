using MeterReadingApi.Application.Interfaces;
using MeterReadingApi.Domain;

namespace MeterReadingApi.Infrastructure
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly MeterReadingContext _context;
        public MeterReadingRepository(MeterReadingContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<MeterReading> readings)
        {
            _context.MeterReadings.AddRange(readings);
        }

        public MeterReading? GetReading(int accountId, DateTime readingDateTime)
        {
            return _context.MeterReadings.FirstOrDefault(x => x.AccountId == accountId && x.MeterReadingDateTime == readingDateTime);
        }
    }

}
