using System;
using System.Collections.Generic;

namespace project_MVC.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";

        // Navigation properties (optional, keep if needed)
        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }
        public List<Product> Products { get; set; }
        public List<Contact> Contact { get; set; }
        public List<Favourite> Favourites { get; set; }
    }
}
