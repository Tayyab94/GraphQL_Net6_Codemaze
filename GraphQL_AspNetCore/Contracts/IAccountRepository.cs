using GraphQL_AspNetCore.Entities;

namespace GraphQL_AspNetCore.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account>GetAllAccountPerOwner(Guid ownerId);

        //Implementing a Cache in the GraphQL Queries with Data Loader

        Task<ILookup<Guid, Account>> GetAccountsByOwnerIds(IEnumerable<Guid> ownerId);
    }
}
