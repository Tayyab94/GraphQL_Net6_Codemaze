using GraphQL_AspNetCore.Contracts;
using GraphQL_AspNetCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_AspNetCore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;
        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }


        public IEnumerable<Account> GetAllAccountPerOwner(Guid ownerId)
                    => _context.Accounts.Where(s => s.OwnerId == ownerId).ToList();



        public async Task<ILookup<Guid, Account>> GetAccountsByOwnerIds(IEnumerable<Guid> ownerId)
        {
            var accounts = await _context.Accounts.Where(s => ownerId.Contains(s.OwnerId)).ToListAsync();

            return accounts.ToLookup(s => s.OwnerId);
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }
    }
}
