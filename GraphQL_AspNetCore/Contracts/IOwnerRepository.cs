using GraphQL_AspNetCore.Entities;

namespace GraphQL_AspNetCore.Contracts
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAll();
        Owner GetById(Guid id);
    }
}
