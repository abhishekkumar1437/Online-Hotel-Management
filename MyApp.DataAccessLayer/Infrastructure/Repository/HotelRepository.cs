using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModels;
using MyAppModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private ApplicationDbContext _context;
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

     

        public void Update(Hotel Hotels)
        {
           var HotelDB = _context.Hotels.FirstOrDefault(x=>x.Id==Hotels.Id);
            if (HotelDB != null)
            {
                HotelDB.Name= Hotels.Name;
                HotelDB.Description= Hotels.Description;
                HotelDB.Price= Hotels.Price;
                if(Hotels.ImageUrl!=null)
                {
                    HotelDB.ImageUrl= Hotels.ImageUrl;
                }
            }
        }

       
    }
}
