using MeterReadingApi.Domain;

namespace MeterReadingApi.Application.Dtos
{
    public record MeterReadingMappingResult(IReadOnlyList<MeterReading> ValidRecords, IReadOnlyList<MeterReadingCsvRecord> InvalidRecords);
}