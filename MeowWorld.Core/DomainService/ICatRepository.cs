using System;
using System.Collections.Generic;
using Meow.Core.Entity;

namespace MeowWorld.Core.DomainService
{
    public interface ICatRepository
    {
        //Create

        Cat Create(Cat cat);


        //Read/Get

        Cat GetCatById(int id);
        IEnumerable<Cat> ReadAllCats();
        //FilteredList<Cat> ReadAll(Filter filter);

        //Update/Put
        Cat Update(Cat catUpdate);

        //Delete
        Cat Delete(int id);
        //List<Cat> GetCats();
        //List<Cat> Filter(string orderDir);
    }
}
