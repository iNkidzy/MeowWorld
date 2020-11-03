using System;
using Meow.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace MeowWorld.Infrastructure.data
{
    public class MEOWcontext:DbContext
    {
        public MEOWcontext(DbContextOptions<MEOWcontext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>()
             .HasOne(p => p.Owner)
             .WithMany(po => po.CatsOwned)
             .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem>TodoItems { get; set; }
       
    }
}
