using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Models.Identity
{
    public class RegistrationRequest
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}

