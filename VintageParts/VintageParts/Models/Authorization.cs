using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Authorization
    {
        [Key]
        public int Auth_id { get; set; }
        [Required(ErrorMessage = "Username required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The login must be at least 3 and not more than 20 characters")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Login must contain only English characters and numbers")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password required")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Password must contain only English characters and numbers")]
        public string Password { get; set; }
        public bool Is_admin { get; set; }
    }
}
