using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : BaseController
    {
        public DashboardController(TicketSystemDbContext context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult AdminPanel()
        {

            return View();
        }

        [Route("Dashboard/AdminPanel/Pool")]
        [HttpGet]
        public async Task<IActionResult> Pool(string page)
        {
            List<Ticket> unassignedTickets = await this.Context.Tickets.Where(u => u.Specialist == null).ToListAsync();

            return View(unassignedTickets);
        }

        [Route("Dashboard/AdminPanel/Ticket/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Ticket(int id)
        {
            Ticket ticket = await this.Context.Tickets.Where(u => u.Id == id).FirstOrDefaultAsync();

            return View(ticket);
        }


        [Route("Dashboard/AdminPanel/Register/SetProfile")]
        [HttpGet]
        public async Task<IActionResult> SetProfile(EditProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var profile = await this.UserProfile;

                profile.Age = model.Age;
                profile.Name = model.Name;

                await this.Context.SaveChangesAsync();

                return RedirectToAction("Profile", "Account", profile);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }
    }
}