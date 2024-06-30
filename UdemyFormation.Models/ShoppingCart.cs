using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyFormation.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int Count { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}