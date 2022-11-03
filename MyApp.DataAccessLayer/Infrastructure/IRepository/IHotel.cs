using MyAppModels;
using MyAppModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        void Update(Hotel Hotels);
      
    }
}
