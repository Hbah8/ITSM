using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public int? RoleId { get; set; }

        public List<Ticket> Tickets { get; set; }

        public UserProfile Profile { get; set; }
    }
    public class Specialist : User
    {

    }

    public class UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
