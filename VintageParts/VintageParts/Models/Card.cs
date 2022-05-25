using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Card
    {
        [Key]
        public int Card_id { get; set; }
        [RegularExpression(@"[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}", ErrorMessage = "Card number does not match the standard")]
        public string CardNumber { get; set; }
        public int User_id { get; set; }
        public User User { get; set; }
        public double Balance { get; set; }
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "CVV code must be 3 digits")]
        public int CvvCode { get; set; }

    }
}
