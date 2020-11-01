using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meow.Core.Entity
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Cat> CatsOwned { get; set; }

    }
}
