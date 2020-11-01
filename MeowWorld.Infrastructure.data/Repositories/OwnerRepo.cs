using System;
using System.Collections.Generic;
using System.Linq;
using MeowWorld.Core.DomainService;
using MeowWorld.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace MeowWorld.Infrastructure.data.Repositories
{
    public class OwnerRepo : IOwnerRepository
    {

        readonly MEOWcontext _ctx;

        public OwnerRepo(MEOWcontext ctx)
        {
            _ctx = ctx;
        }

        public Owner Create(Owner owner)
        {
            Owner ow = _ctx.Owners.Add(owner).Entity;
            _ctx.SaveChanges();
            return ow;
        }

        public Owner Delete(int id)
        {
            Owner o = ReadAllOwners().ToList().Find(x => x.Id == id);
            ReadAllOwners().ToList().Remove(o);
            if (o != null)
            {

                return o;
            }
            return null;
        }

        Owner IOwnerRepository.GetByIdIncludingCats(int id)
        {
            return _ctx.Owners.Include(o => o.CatsOwned).FirstOrDefault(o => o.Id == id);
        }

        public Owner GetOwnerById(int id)
        {
            return _ctx.Owners
                .AsNoTracking()
                .FirstOrDefault(o => o.Id == id);

        }

        public IEnumerable<Owner> ReadAllOwners()
        {
            return _ctx.Owners.Include(o => o.CatsOwned).ToList();
        }

        public Owner Update(Owner ownerUpdate)
        {
            var avatarFromDB = this.GetOwnerById(ownerUpdate.Id);
            if (avatarFromDB != null)
            {
                avatarFromDB.FirstName = ownerUpdate.FirstName;
                avatarFromDB.LastName = ownerUpdate.LastName;
                avatarFromDB.PhoneNumber = ownerUpdate.PhoneNumber;
                avatarFromDB.Email = ownerUpdate.Email;


                return avatarFromDB;
            }

            return null;
        }
    }
}
