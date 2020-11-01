using System;
namespace Meow.Core.Entity
{
    public class Cat
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Price { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
