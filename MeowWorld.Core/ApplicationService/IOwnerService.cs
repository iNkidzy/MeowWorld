using System;
using System.Collections.Generic;
using Meow.Core.Entity;

namespace MeowWorld.Core.ApplicationService
{
    public interface IOwnerService
    {
        Owner NewOwner(string firstName, string lastName, string phoneNumber,
                       string email);

        Owner Create(Owner owner);

        Owner FindOwnerById(int id);
        List<Owner> ReadAllOwners();

        //List<Owner> GetOwner(); //DELETEMAYBE

        Owner UpdateOwner(Owner ownerUpdate);

        Owner DeleteOwner(int id);

    }
}
