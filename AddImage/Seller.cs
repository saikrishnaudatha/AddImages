using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddingImages.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CompName { get; set; }
        public string Gstin { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public int Phno { get; set; }
        public string PhotoPath { get; set; }




    }
}
