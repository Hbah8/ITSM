﻿using System;
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

        [Route("Dashboard/AdminPanel/Register/SetProfile/{id?}")]
        [HttpGet]
        public async Task<IActionResult> SetProfile(int id)
        {
            var currentUser = await this.Context.Users.Where(u => u.Email == User.Identity.Name).Include(u => u.Role).FirstOrDefaultAsync();

            var user = await this.Context.Users.Where(u => u.Id == id).Include(p => p.Profile).FirstOrDefaultAsync();
            if (currentUser.Role?.Name == "admin" || currentUser.Role?.Name == "specialist")
            {
                ViewData["Идентификатор"] = id;
                ViewData["Имя пользователя"] = user.Profile?.Name;
                ViewData["Возраст пользователя"] = user.Profile?.Age;

                return View();
            }

            else {
                this.Context.Users.Remove(user);
                await this.Context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }
    }
}