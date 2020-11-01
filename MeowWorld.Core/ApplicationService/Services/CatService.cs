using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MeowWorld.Core.DomainService;
using MeowWorld.Core.Entity;

namespace MeowWorld.Core.ApplicationService.Services
{
    public class CatService:ICatService
    {
        readonly ICatRepository _catRepo;

        public CatService(ICatRepository catRepository)
        {
            _catRepo = catRepository;
        }

        public Cat NewCat(string name, string type, string color, DateTime createdDate, double price)
        {
            var cat = new Cat()
            {
                Name = name,
                Type = type,
                Color = color,
                CreatedDate = createdDate,
                Price = price

            };

            return cat;
        }


        public Cat CreateCat(Cat cat)
        {
            return _catRepo.Create(cat);
        }

       

        public Cat FindCatById(int id)
        {
            return _catRepo.GetCatById(id);
        }

        

        public List<Cat> ReadAllCats()
        {
            return _catRepo.ReadAllCats().ToList();
        }

        public Cat UpdateCat(Cat catUpdate) //Could give errors
        {
            if (catUpdate.Name.Length < 1)
            {
                throw new InvalidDataException("Name must be atleast 1 charecter");
            }
            else
            {
                var cat = FindCatById(catUpdate.Id);
                cat.Name = catUpdate.Name;
                cat.Type = catUpdate.Type;
                cat.Color = catUpdate.Color;
                cat.CreatedDate = catUpdate.CreatedDate;
                cat.Price = catUpdate.Price;
                return cat;
            }
           // return _catRepo.Update(cat);
            
            
        }


        public Cat DeleteCat(int id)
        {
            return _catRepo.Delete(id);
        }
    }
}
