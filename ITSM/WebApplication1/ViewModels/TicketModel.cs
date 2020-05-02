using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    [Authorize]
    public class TicketModel
    {
        [Required(ErrorMessage = "Не введено краткое описание")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Не введено описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите приоритет")]
        public Priority Priority { get; set; }


    }
}
