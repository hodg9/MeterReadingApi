## Overview
This API accepts CSV files containing meter readings, validates them against known accounts
and business rules, and persists valid entries. A summary of successes and failures is returned.

## Endpoints
- **POST /meter-reading-uploads**  
  Content-Type: `multipart/form-data`  
  Upload a CSV file of meter readings.

Response:
```json
{ "totalRecords": 15, "successfulRecords": 10, "failedRecords": 5 }
{ "error": "Invalid file format. Only .csv files are supported." }
```

Swagger documentation is available at `/swagger` when the application is running.

### Assumptions from the requirements
- _You should not be able to load the same entry twice_ 
  - this is interpreted as:
    - Duplicate entries in the same file should be ignored
    - Entries that already exist in the database should be ignored
- Meter readings for the future are invalid

### Tech Stack
- .NET 8 Web API
- Entity Framework Core with SQL Server
- xUnit with Moq for unit tests
- Swagger for API documentation

### Notes
- The DbContext is considered a Unit of Work for a larger application I would abstract behind IUnitOfWork interface
- I've chosen to duplicate the validation logic to avoid using exceptions for decisions.
- Index added for AccountId and MeterReadingDateTime to improve performance when checking for existing readings.
- I chose not to include a React frontend so I could focus on the API within the timebox. If extended, I would have built a small frontend using
   - React using Vite and SWC
   - Material UI for a clean UX
   - React Query for API calls
 - The project is split in to folder that could be separate projects in a larger solution (API, Application, Domain, Infrastructure)
 - I aimed for around half a day as suggested, though it went a little over, but wanted to add a little more structure.
### Testing
- Unit tests cover validation logic (value format, account existence, duplicates, future dates)
- Endpoints check for missing, empty, and invalid files

### Next Steps
- Authentication and Authorization
- Logging and Monitoring
- Containerization with Docker