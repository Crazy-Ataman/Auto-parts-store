using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Provider
    {
        [Key]
        public int Provider_id { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^([a-zA-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

    }
}
