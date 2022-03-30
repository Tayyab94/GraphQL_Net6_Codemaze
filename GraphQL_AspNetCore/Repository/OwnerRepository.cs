using GraphQL_AspNetCore.Contracts;
using GraphQL_AspNetCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_AspNetCore.Repository
{
    public class OwnerRepository: IOwnerRepository
    {
        private readonly ApplicationContext _context;
        public OwnerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Owner> GetAll() => _context.Owners.ToList();

        public Owner GetById(Guid id)
        {
            return _context.Owners.SingleOrDefault(s => s.Id == id);
        }
    }
}
