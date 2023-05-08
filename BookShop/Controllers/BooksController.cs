using BookShop.Application;
using BookShop.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.IO;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<GenresController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookGenreRepository _bookGenreRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;


        public BooksController(
            ILogger<GenresController> logger,
            IBookRepository bookRepository,
            ICategoryRepository categoryRepository,
            IAuthorRepository authorRepository,
            IBookGenreRepository bookGenreRepository,
            IBookAuthorRepository bookAuthorRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookGenreRepository = bookGenreRepository;
            _bookAuthorRepository = bookAuthorRepository;
        }

        // GET: BookController
        public async Task<ActionResult> Index()
        {
            // Переделать через ViewModel
            return View(await _bookRepository.GetFullData());
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookController/Create
        public async Task<ActionResult> Create(int id)
        {
            CreateBookViewModel bookModel = new CreateBookViewModel()
            {
                Id = id,
                Book = await _bookRepository.GetById(id),
                Authors = await _authorRepository.GetAll(),
                Genres = await _categoryRepository.GetAll()
            };
            //IEnumerable<Author> authors = await _authorRepository.GetAll();
            //ViewBag.AuthorsList = authors;
            //ViewBag.GenresList = await _categoryRepository.GetAll();

            ViewBag.SelectListGenres = new SelectList(await _categoryRepository.GetAll(), "Id", "Name");
            ViewBag.SelectListAuthors = (IEnumerable<SelectListItem>)_authorRepository.ToSelectListItems(bookModel.Authors, 0);


            return View(bookModel);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book, int genreId, int authorId, IFormCollection collection)
        {
            try
            {
                await _bookRepository.Insert(book);
                await _bookGenreRepository.Insert(new BookGenre
                {
                    Book = book,
                    Genre = await _categoryRepository.GetById(genreId)
                });
                await _bookAuthorRepository.Insert(new BookAuthor
                {
                    Book = book,
                    Author = await _authorRepository.GetById(authorId)
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
