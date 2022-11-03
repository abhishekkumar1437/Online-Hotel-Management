using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IHotelRepository Hotel { get; }
        ICartRepository Cart { get; }
        IHistoryRepository History { get; }
        IApplicationUser ApplicationUser { get; }
        public void Save();
    }

}
