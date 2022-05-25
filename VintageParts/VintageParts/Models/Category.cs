using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Category
    {
        [Key]
        public int Category_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Part> Parts { get; set; }
    }
}
