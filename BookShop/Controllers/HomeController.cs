using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookShop.Application;
using BookShop.Infrastructure;
using BookShop.Filters;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }
        [LastVisit]
        public IActionResult Index()
        {

            _logger.LogInformation("index");
            if (1 > 2)
                _logger.LogWarning("1 > 2 ??");
            try
            {
                throw new Exception("ACD");
            }
            catch(Exception ex)
            {
                _logger.LogError("throw new exception", ex);
            }

            ViewBag.Categories = _categoryRepository.GetAll();

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}