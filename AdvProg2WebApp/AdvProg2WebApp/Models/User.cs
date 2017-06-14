using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdvProg2WebApp.Models
{
    public class User
    {
        [Key]
        public string UserNameId { get; set; }
        [Required]
        public int Losses { get; set; }
        [Required]
        public int Victories { get; set; }
    }
}