using MeterReadingApi.Domain;

namespace MeterReadingApi.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MeterReadingContext _context;

        public AccountRepository(MeterReadingContext context)
        {
            _context = context;
        }

        public Account? GetById(int id)
        {
            return _context.Accounts.Find(id);
        }
    }

}
