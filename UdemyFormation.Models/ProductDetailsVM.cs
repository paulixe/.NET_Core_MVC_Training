using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyFormation.Models
{
    public class ProductDetailsVM
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}