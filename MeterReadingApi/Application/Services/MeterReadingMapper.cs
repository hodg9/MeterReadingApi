using MeterReadingApi.Application.Dtos;
using MeterReadingApi.Application.Interfaces;
using MeterReadingApi.Domain;

namespace MeterReadingApi.Application.Services
{
    public class MeterReadingMapper
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMeterReadingRepository _meterReadingRepository;

        public MeterReadingMapper(IAccountRepository accountRepository, IMeterReadingRepository meterReadingRepository)
        {
            _accountRepository = accountRepository;
            _meterReadingRepository = meterReadingRepository;
        }

        private bool IsValid(MeterReadingCsvRecord record, List<MeterReading> validRecords)
        {
            //Meter Reading format must be NNNNN
            if (record.MeterReadValue < 0 || record.MeterReadValue > 99999) return false;
            
            //Assumption: Meter reading can not be for the future?
            if (record.MeterReadingDateTime > DateTime.Now) return false;
            
            //Account must exist
            var account = _accountRepository.GetById(record.AccountId);
            if (account is null) return false;
            
            //No duplicate meter reading for the same account at the same date time
            var existingReading = _meterReadingRepository.GetReading(record.AccountId, record.MeterReadingDateTime);
            if (existingReading is not null) return false;
            
            //No duplicate meter reading in the same upload for the same account at the same date time
            var duplicateInUpload = validRecords.FirstOrDefault(r => r.AccountId == record.AccountId && r.MeterReadingDateTime == record.MeterReadingDateTime);
            if (duplicateInUpload is not null) return false;
            
            return true;
        }

        private MeterReading Map(MeterReadingCsvRecord record)
        {
            return new MeterReading(record.AccountId, record.MeterReadingDateTime, record.MeterReadValue);
        }

        public MeterReadingMappingResult Map(IEnumerable<MeterReadingCsvRecord> records)
        {
            var validRecords = new List<MeterReading>();
            var invalidRecords = new List<MeterReadingCsvRecord>();
            foreach (var record in records)
            {
                if (IsValid(record, validRecords))
                {
                    validRecords.Add(Map(record));
                }
                else
                {
                    invalidRecords.Add(record);
                }
            }
            return new MeterReadingMappingResult(validRecords, invalidRecords);
        }

    }
}