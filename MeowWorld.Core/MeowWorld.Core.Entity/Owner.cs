using System;
using System.Collections.Generic;

namespace MeowWorld.Core.Entity
{
    public class Owner
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Cat> CatsOwned { get; set; }

    }
}
