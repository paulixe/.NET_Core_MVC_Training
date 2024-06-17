using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UdemyFormation.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 1000)]
        public double Price { get; set; }

        [Range(0, 1000)]
        public double Price10 { get; set; }
    }
}