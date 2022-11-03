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
    public class HistoryRepository : Repository<History>, IHistoryRepository
    {
        private ApplicationDbContext _context;
        public HistoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
