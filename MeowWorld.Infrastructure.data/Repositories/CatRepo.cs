using System.Collections.Generic;
using System.Linq;
using Meow.Core.Entity;
using MeowWorld.Core.DomainService;
using Microsoft.EntityFrameworkCore;

namespace MeowWorld.Infrastructure.data.Repositories
{
    public class CatRepo : ICatRepository
    {
        readonly MEOWcontext _ctx;

        public CatRepo(MEOWcontext ctx)
        {
            _ctx = ctx;
        }

        public Cat Create(Cat cat)
        {
            Cat a = _ctx.Cats.Add(cat).Entity;
            _ctx.SaveChanges();
            return a;
        }

        public Cat Delete(int id) //CheckTHIS
        {
            Cat a = ReadAllCats().ToList().Find(x => x.Id == id);
            ReadAllCats().ToList().Remove(a);
            if (a != null)
            {

                return a;
            }
            return null;
        }
    

        public Cat GetCatById(int id)
        {
          return _ctx.Cats
            .AsNoTracking()
            .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Cat> ReadAllCats()
        {
            return _ctx.Cats.Include(o => o.Owner).ToList();
        }

        public Cat Update(Cat catUpdate)
        {
            var catDB = GetCatById(catUpdate.Id);
            if (catDB != null)
            {
                catDB.Name = catUpdate.Name;
                catDB.Type = catUpdate.Type;
                catDB.CreatedDate = catUpdate.CreatedDate;
                catDB.Color = catUpdate.Color;
                catDB.Price = catUpdate.Price;

                return catDB;
            }

            return null;
        }
    }
  
}
