using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppModels.ViewModels
{
    public class HistoryVM
    {
        public IEnumerable<History> ListOfHistory { get; set; }    
        public double Total { get; set; }   
    }
}
