using MeterReadingApi.Domain;

namespace MeterReadingApi.Application.Interfaces
{
    public interface IAccountRepository
    {
        Account? GetById(int id);
    }

}