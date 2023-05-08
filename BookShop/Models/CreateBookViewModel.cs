using BookShop.Domain;

namespace BookShop.Models
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }

        public Book Book{ get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
