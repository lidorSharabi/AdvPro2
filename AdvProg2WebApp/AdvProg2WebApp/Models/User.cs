using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdvProg2WebApp.Models
{
    /// <summary>
    /// model class for the user
    /// </summary>
    public class User
    {
        /// <summary>
        /// the id of the user
        /// </summary>
        [Key]
        public string UserNameId { get; set; }
        /// <summary>
        /// number of lost games
        /// </summary>
        [Required]
        public int Losses { get; set; }
        /// <summary>
        /// number of won games
        /// </summary>
        [Required]
        public int Victories { get; set; }
        /// <summary>
        /// user mail address
        /// </summary>
        [Required]
        public string MailAddress { get; set; }
        /// <summary>
        /// user password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}