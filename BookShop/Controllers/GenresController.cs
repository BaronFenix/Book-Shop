using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookShop.Infrastructure;
using BookShop.Application;
using BookShop.Domain;
using System.Collections.Generic;
using System.Linq;
using BookShop.Filters;

namespace BookShop.Controllers
{
    public class GenresController : Controller
    {
        //private readonly IStoreService store;
        private readonly ILogger<GenresController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;

        public GenresController(
            IBookRepository bookRepository,
            ILogger<GenresController> logger,
            ICategoryRepository categoryRepository)
        {
            //this.store = store;
            this._logger = logger;
            this._categoryRepository = categoryRepository;
            this._bookRepository = bookRepository;
        }

        // GET: GenresController
        public async Task<ActionResult> Index()
        {
            ViewBag.Categories = await _categoryRepository.GetAll();

            return View(await _categoryRepository.GetAll());
        }
        
        public async Task<ActionResult> Genre(int id)
        {
            ViewBag.BooksList = await _bookRepository.GetBooksFromCategory(id);

            return View(await _categoryRepository.GetById(id));
        }

        // GET: GenresController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _categoryRepository.GetById(id));
        }

        // GET: GenresController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: GenresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Genre newGenre)
        {
            try
            {
                await _categoryRepository.Insert(newGenre);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenresController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Genre genre = await _categoryRepository.GetById(id);
            return View(genre);
        }

        // POST: GenresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Genre editedGenre, IFormCollection collection)
        {
            try
            {
                await _categoryRepository.Update(editedGenre);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenresController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Genre genre = await _categoryRepository.GetById(id);
            if (genre == null)
            {
                return RedirectToRoute("Share", "Error");
            }
            return View(genre);

        }

        // POST: GenresController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            await _categoryRepository.Delete(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
