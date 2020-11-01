using System;
using System.Collections.Generic;
using MeowWorld.Core.Entity;

namespace MeowWorld.Core.ApplicationService
{
    public interface ICatService
    {
        //Add New cat
        Cat NewCat(string name, string type, string color,
                   DateTime createdDate, double price);

        //Create
        Cat CreateCat(Cat cat);

        //Read/Get
        Cat FindCatById(int id);
        List<Cat> ReadAllCats();

        // List<Cat> GetCats(); //DeleteThisIDK

        //Filtering
        //public List<Cat> CatsByPrice();

        // public List<Cat> CheapestCats();

        //FilteredList<Customer> GetAllCustomers(Filter filter);

        //Update

        Cat UpdateCat(Cat catUpdate);

        //Delete

        Cat DeleteCat(int id);

    }
}
