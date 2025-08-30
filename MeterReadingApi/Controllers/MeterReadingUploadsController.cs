using MeterReadingApi.Domain;
using MeterReadingApi.Dtos;
using MeterReadingApi.Infrastructure;
using MeterReadingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeterReadingApi.Controllers
{
    [ApiController]
    [Route("/meter-reading-uploads")]
    public class MeterReadingUploadsController : ControllerBase
    {
        private readonly MeterReadingContext _meterReadingContext;
        private readonly MeterReadingMapper _meterReadingMapper;
        private readonly IMeterReadingRepository _meterReadingRepository;

        public MeterReadingUploadsController(MeterReadingContext meterReadingContext, MeterReadingMapper meterReadingMapper, IMeterReadingRepository meterReadingRepository)
        {
            _meterReadingContext = meterReadingContext;
            _meterReadingMapper = meterReadingMapper;
            _meterReadingRepository = meterReadingRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload(IFormFile meterReadingsCsv)
        {
            ArgumentNullException.ThrowIfNull(meterReadingsCsv);

            //Check file is not empty
            if (meterReadingsCsv.Length == 0)
            {
                return BadRequest("The uploaded file is empty.");
            }

            //Ensure it's a CSV file
            if (!Path.GetExtension(meterReadingsCsv.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file format. Only .csv files are supported.");
            }
            
            MeterReadingCsvParserResult parsedResults;
            try
            {
                //Pocess the CSV file
                var uploadService = new MeterReadingCsvParser();
                using var stream = meterReadingsCsv.OpenReadStream();
                parsedResults = await uploadService.ParseCsvAsync(stream);
            }
            catch (Exception)
            {
                return BadRequest($"Failed to parse CSV.");
            }

            //Map and validate the records
            var mappingResults = _meterReadingMapper.Map(parsedResults.Records);

            try
            {
                //Persist the valid records
                if (mappingResults.ValidRecords.Any())
                {
                    _meterReadingRepository.AddRange(mappingResults.ValidRecords);
                    await _meterReadingContext.SaveChangesAsync(); //should be swapped out for a UnitOfWork in a larger app
                }
            }
            catch (Exception)
            {
                return BadRequest($"Failed to save meter readings.");
            }

            return Ok(new MeterReadingUploadResult
            {
                TotalRecords = parsedResults.Records.Count + parsedResults.BadRows.Count,
                SuccessfulRecords = mappingResults.ValidRecords.Count,
                FailedRecords = mappingResults.InvalidRecords.Count + parsedResults.BadRows.Count
            });
        }

    }
}
