using BookShop.Application;
using BookShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.Repositories
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
    {
        private readonly ApplicationContext _context;
        public BookAuthorRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
