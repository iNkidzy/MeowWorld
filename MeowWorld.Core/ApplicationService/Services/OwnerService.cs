using System;
using System.Collections.Generic;
using System.Linq;
using MeowWorld.Core.DomainService;
using Meow.Core.Entity;

namespace MeowWorld.Core.ApplicationService.Services
{
    public class OwnerService:IOwnerService
    {
        readonly IOwnerRepository _ownerRepo;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepo = ownerRepository;
        }

        public Owner NewOwner(string firstName, string lastName, string phoneNumber, string email)
        {
            var owner = new Owner()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email
            };
            return owner;
        }

        public Owner Create(Owner owner)
        {
            return _ownerRepo.Create(owner); 
        }

       

        public Owner FindOwnerById(int id)
        {
            return _ownerRepo.GetOwnerById(id);
        }

        public Owner ReadByIdIncludingAvatars(int id)
        {
            var owner = _ownerRepo.GetByIdIncludingCats(id);
            return owner;
        }


        public List<Owner> ReadAllOwners()
        {
            return _ownerRepo.ReadAllOwners().ToList();
        }

        public Owner UpdateOwner(Owner ownerUpdate)
        {
            var DBowner = FindOwnerById(ownerUpdate.Id);
            if (DBowner != null)
            {

                DBowner.FirstName = ownerUpdate.FirstName;
                DBowner.LastName = ownerUpdate.LastName;
                DBowner.PhoneNumber = ownerUpdate.PhoneNumber;
                DBowner.Email = ownerUpdate.Email;


            }

           return DBowner;
            
        }

        public Owner DeleteOwner(int id)
        {
            return _ownerRepo.Delete(id);
        }
    }
}
