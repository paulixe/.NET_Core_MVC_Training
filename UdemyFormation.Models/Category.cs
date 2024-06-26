﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UdemyFormation.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}