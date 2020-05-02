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
    [Authorize]
    public class TicketController : BaseController
    {

        public TicketController(TicketSystemDbContext context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TicketModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                var user = await this.Context.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefaultAsync();

                var ticket = new Ticket()
                {
                    User = user,
                    ShortDescription = model.ShortDescription,
                    Description = model.Description,
                    Priority = model.Priority,
                    Status = Status.New
                };

                this.Context.Tickets.Add(ticket);

                await this.Context.SaveChangesAsync();

                return RedirectToAction("MyTickets", "Ticket");
            }

            return View(model);
        }


        [Route("/Ticket/MyTickets/")]
        [HttpGet]
        public async Task<IActionResult> MyTickets()
        {
            List<Ticket> userTickets = await this.Context.Tickets.Where(u => u.User.Email == User.Identity.Name).ToListAsync();

            return View(userTickets);
        }

        [Route("/Ticket/MyTickets/{id?}")]
        [HttpGet]
        public async Task<IActionResult> MyTickets(int? id)
        {
            var ticket = await this.Context.Tickets.Include(u => u.User).Where(u => u.Id == id).FirstOrDefaultAsync();

            if (id != 0 && User.Identity.Name == ticket.User?.Email)
            {
                return RedirectToAction("Ticket", ticket);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Ticket(Ticket ticket)
        {
            ticket.User = await this.Context.Users.Where(u => u.Id == ticket.UserId).FirstOrDefaultAsync();

            return View(ticket);
        }

        [HttpGet]
        [Route("Dashboard/AdminPanel/Ticket/{id}/Assign")]
        public async Task<IActionResult> Assign(int id)
        {
            var freeSpecialists = await this.Context.Users.Where(u => u.Role.Name == "specialist").ToListAsync();

            return View(freeSpecialists);
        }
    }
}