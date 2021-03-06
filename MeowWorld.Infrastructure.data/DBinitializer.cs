﻿    using System;
using System.Collections.Generic;
using Meow.Core.Entity;
using Microsoft.EntityFrameworkCore.Internal;

namespace MeowWorld.Infrastructure.data
{
    public class DBinitializer :IDBinitializer
    {
        private IAuthenticationHelper authenticationHelper;

        public DBinitializer(IAuthenticationHelper authHelper)
        {
            authenticationHelper = authHelper;
        }

        public void InitData(MEOWcontext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            if (ctx.TodoItems.Any())
            {
                return;
            }

            List<TodoItem> items = new List<TodoItem>
            {
                new TodoItem { IsComplete=true, Name="I did it!!"},
                new TodoItem { IsComplete=false, Name="Failed...again"},
                new TodoItem { IsComplete=false, Name="<h3>Message from a Black Hat! Ha, ha, ha...<h3>"}
            };

            string password = "nadia";
            byte[] passwordHashChili, passwordSaltChili, passwordHashNadia, passwordSaltNadia;

            authenticationHelper.CreatePasswordHash(password, out passwordHashChili, out passwordSaltChili);
            authenticationHelper.CreatePasswordHash(password, out passwordHashNadia, out passwordSaltNadia);

            List<User> users = new List<User>
            {
                new User {
                    Username = "UserChili",
                    PasswordHash = passwordHashChili,
                    PasswordSalt = passwordSaltChili,
                    IsAdmin = false
                },
                new User {
                    Username = "AdminNadia",
                    PasswordHash = passwordHashNadia,
                    PasswordSalt = passwordSaltNadia,
                    IsAdmin = true
                }
            };



            Owner owner1 = ctx.Owners.Add(new Owner
            {
                FirstName = "Honey",
                LastName = "Bunny",
                PhoneNumber = "42213184",
                Email = "Bunny@gmail.com",


            }).Entity;

            Owner owner2 = ctx.Owners.Add(new Owner
            {
                FirstName = "Nadia",
                LastName = "Bunny",
                PhoneNumber = "42213184",
                Email = "Bunny@gmail.com",

            }).Entity;


            Owner owner3 = ctx.Owners.Add(new Owner
            {
                FirstName = "Niki",
                LastName = "Bunny",
                PhoneNumber = "42213184",
                Email = "Bunny@gmail.com",


            }).Entity;


            Owner owner4 = ctx.Owners.Add(new Owner
            {
                FirstName = "Chili",
                LastName = "Bunny",
                PhoneNumber = "42213184",
                Email = "Bunny@gmail.com",


            }).Entity;



            Random r = new Random();
            Cat cat1 = ctx.Cats.Add(new Cat
            {

                Name = "Kitty",
                Type = "Turkish Angora",
                CreatedDate = DateTime.Now.AddYears(-15),
                Color = "White",
                Price = 2000,
                Owner = owner1


            }).Entity;

            Cat cat2 = ctx.Cats.Add(new Cat
            {

                Name = "Alec",
                Type = "Bombay",
                CreatedDate = DateTime.Now.AddYears(-15),
                Color = "Black",
                Price = 2900,
                Owner = owner2


            }).Entity;

            Cat cat3 = ctx.Cats.Add(new Cat
            {

                Name = "Rocky",
                Type = "Cheetoh",
                CreatedDate = DateTime.Now.AddYears(-15),
                Color = "TigerOrange",
                Price = 2500,
                Owner = owner3


            }).Entity;

            Cat cat4 = ctx.Cats.Add(new Cat
            {

                Name = "Luc",
                Type = "British",
                CreatedDate = DateTime.Now.AddYears(-15),
                Color = "Grey",
                Price = 2150,
                Owner = owner2


            }).Entity;

            Cat cat5 = ctx.Cats.Add(new Cat
            {

                Name = "Pinky",
                Type = "Munchkin",
                CreatedDate = DateTime.Now.AddYears(-15),
                Color = "Pink",
                Price = 2450,
                Owner = owner4


            }).Entity;

            ctx.TodoItems.AddRange(items);
            ctx.Users.AddRange(users);
            

            ctx.SaveChanges();
        }
    }
}
