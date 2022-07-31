using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_api.Models
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [Compare("ConfrimPassowrd")]
        public string Passowrd { get; set; }

        [Required]
        public string ConfrimPassowrd { get; set; }
    }
}
