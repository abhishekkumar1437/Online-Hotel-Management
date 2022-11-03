using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppModels.ViewModels
{
    public class HotelVM
    {
        
        public Hotel Hotel { get; set; }  = new Hotel();
        [ValidateNever]
        public IEnumerable<Hotel> Hotels { get; set; } = new List<Hotel>();
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
