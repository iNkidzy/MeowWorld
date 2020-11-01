using System;
using System.Collections.Generic;
using MeowWorld.Core.Entity;

namespace MeowWorld.Core.DomainService
{
    public interface IOwnerRepository
    {
        //Create
        Owner Create(Owner owner);

        //Read/Get

        Owner GetOwnerById(int id);
        IEnumerable<Owner> ReadAllOwners();

        //Update
        Owner Update(Owner ownerUpdate);

        //Delete    
        Owner Delete(int id);
        Owner GetByIdIncludingCats(int id);
        
    }
}
