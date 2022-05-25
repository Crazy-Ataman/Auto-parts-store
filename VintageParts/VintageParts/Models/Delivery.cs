using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Delivery
    {
        [Key]
        public int Delivery_id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }

    }
}
