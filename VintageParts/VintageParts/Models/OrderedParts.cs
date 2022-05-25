using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class OrderedParts
    {
        public int Order_id { get; set; }
        public Order Order { get; set; }
        public int Part_id { get; set; }
        public Part Part { get; set; }
        public int Amount { get; set; }
    }
}
