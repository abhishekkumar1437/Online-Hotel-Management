using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyAppModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppModels
{
    public class Hotel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; } 
        public string Location { get; set; }    

        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

    }
}
