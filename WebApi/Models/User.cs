using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User
    {
        //TODO:MAYBE WE NEED CONSTRUCTOR
        [Key]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}