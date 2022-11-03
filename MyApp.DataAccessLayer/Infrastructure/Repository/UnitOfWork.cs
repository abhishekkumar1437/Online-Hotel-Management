using MyApp.DataAccessLayer.Data;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public IHotelRepository Hotel { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IHistoryRepository History { get; private set; }
        public IApplicationUser ApplicationUser { get; private set; }

        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Category = new CategoryRepository(context);
            Hotel = new HotelRepository(context);
            Cart = new CartRepository(context);
            History=new HistoryRepository(context);
            ApplicationUser = new ApplicationRepository(context);   
        }
       

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
