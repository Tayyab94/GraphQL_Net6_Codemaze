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


        public Owner CreateOwner(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            _context.Owners.Add(owner);
            _context.SaveChanges();
            return owner;
        }

        public Owner UpdateOwner(Owner dbOwner, Owner owner)
        {
            dbOwner.Name = owner.Name;
            dbOwner.Address= owner.Address;
            _context.SaveChanges();

            return dbOwner;
        }

        public void DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            _context.SaveChanges();
        }
    }
}
