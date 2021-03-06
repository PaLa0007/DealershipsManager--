﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public string Address { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
