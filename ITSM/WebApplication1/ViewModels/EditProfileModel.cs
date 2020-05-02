using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class EditProfileModel
    {
        
        public int UserId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }


    }
}
