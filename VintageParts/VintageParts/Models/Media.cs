using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Media
    {
        [Key]
        public int Media_id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Part> Parts { get; set; }
    }
}
