using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterReadingApi.Domain
{
    public class MeterReading
    {
        [Key]
        public int Id { get; private set; }
        public int AccountId { get; private set; }
        public DateTime MeterReadingDateTime { get; private set; }
        public int MeterReadValue { get; private set; }

        private MeterReading() //For EF Core
        {            
        }

        public MeterReading(int accountId, DateTime meterReadingDateTime, int meterReadValue)
        {
            //Validation
            if (meterReadValue < 0 || meterReadValue > 99999) throw new ArgumentOutOfRangeException("Reading values should be in the format NNNNN");
            if (meterReadingDateTime > DateTime.Now) throw new ArgumentOutOfRangeException("Meter reading date cannot be in the future");
            AccountId = accountId;
            MeterReadingDateTime = meterReadingDateTime;
            MeterReadValue = meterReadValue;
        }

    }
}
