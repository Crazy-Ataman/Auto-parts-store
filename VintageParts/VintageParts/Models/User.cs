using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        public int Auth_id { get; set; }
        [RegularExpression(@"^[A-Z]{1}[a-z]+", ErrorMessage = "The name must start with a capital letter and be written in Latin")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[A-Z]{1}[a-z]+", ErrorMessage = "Surname must start with a capital letter and be written in Latin")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public Authorization Authorization { get; set; }
    }
}
