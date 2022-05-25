using VintageParts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Models
{
    public class Part : ViewModelBase
    {
        [Key]
        public int Part_id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Provider_id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public int Media_id { get; set; }
        public Media Media { get; set; }
        public int Brand_id { get; set; }
        public Brand Brand { get; set; }
        public int Category_id { get; set; }
        public Category Category { get; set; }
        public Provider Provider { get; set; }
        [NotMapped]
        public int Amount {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        [NotMapped]
        private int amount;
    }
}
