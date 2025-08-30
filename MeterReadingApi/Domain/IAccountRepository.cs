namespace MeterReadingApi.Domain
{
    public interface IAccountRepository
    {
        Account? GetById(int id);
    }

}