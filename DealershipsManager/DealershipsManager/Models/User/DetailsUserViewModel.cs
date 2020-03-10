using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Models.User
{
    public class DetailsUserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
