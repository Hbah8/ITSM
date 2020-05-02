using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        private static TicketSystemDbContext db;

        public TicketSystemDbContext Context
        {
            get { return db; }
        }

        public BaseController(TicketSystemDbContext context)
        {
            db = context;
        }

        public Task<UserProfile> UserProfile
        {
            get {
                return GetUserProfile();
            }
        }

        private async Task<UserProfile> GetUserProfile()
        {
            var user = await db.Users.Where(u => u.Email == User.Identity.Name).Include(u => u.Profile).FirstOrDefaultAsync();

            if(user.Profile == null)
            {
                var profile = new UserProfile();
                this.Context.Add(profile);

                user.Profile = profile;
                await this.Context.SaveChangesAsync();
            }

            return user.Profile;
        }
    }
}