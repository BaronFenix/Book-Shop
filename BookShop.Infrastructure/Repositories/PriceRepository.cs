using BookShop.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain;

namespace BookShop.Infrastructure.Repositories
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        private readonly ApplicationContext _context;
        public PriceRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
