using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }
        public int? PriorityId { get; set; }

        public Status Status { get; set; }


        public int? SpecialistId { get; set; }
        public Specialist Specialist { get; set; }


        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
