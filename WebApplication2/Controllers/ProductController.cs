using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using MVCApplication.Models;
using System.Diagnostics;
using WebApiApplication.DAO;

namespace WebApiApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private NewDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, NewDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Index()
        {
            return View(await _dbContext.Products.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<Product>> InputForm(int id) =>
         View(await _dbContext.Products.FirstOrDefaultAsync(product => product.ProductId == id));


        [HttpPost]
        public async Task<IActionResult> InputForm(Product product)
        {
            if (!product.ProductId.Equals(_dbContext.Products.FirstOrDefaultAsync()))
            {
                _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
            }
            else { Console.WriteLine("Use another Product Id"); }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult<Product>> EditProduct(int id) =>
         View(await _dbContext.Products.FirstOrDefaultAsync(product => product.ProductId == id));

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            _dbContext.Entry(await _dbContext.Products
                    .FirstOrDefaultAsync(dbUser => dbUser.ProductId == product.ProductId))
                .CurrentValues.SetValues(product);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}