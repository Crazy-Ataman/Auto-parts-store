using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Order
    {
        [Key]
        public int Order_id { get; set; }
        public DateTime OrderDate { get; set; } 
        public List<OrderedParts> Parts { get; set; }
        public string OrderState { get; set; }
        public int User_id { get; set; }  
        public User User { get; set; }
        public int Delivery_id { get; set; }
        public Delivery Delivery { get; set; }
    }
}
