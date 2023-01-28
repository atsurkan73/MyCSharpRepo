using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApplication.Models;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewDbContext _dbContext;
        public List<UserLogin> MockUsers = new List<UserLogin>
        { 
            new UserLogin(){Id = 1, UserName = "user1", Password = "pass@1", Role = "Admin"},
            new UserLogin(){Id = 2, UserName = "user2", Password = "pass@2", Role = "Support"},
            new UserLogin(){Id = 3, UserName = "user3", Password = "pass@3", Role = "Customer"}
        };

        public HomeController(ILogger<HomeController> logger, NewDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet ]
        
        public async Task<ActionResult<List<Product>>> Index()
        {
            return View(await _dbContext.Products.ToListAsync());
        }

        public ActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin login)
        {
        var dbUser = MockUsers
            .FirstOrDefault(user => user.UserName == login.UserName
            && user.Password == login.Password);
        
            if (dbUser is not null)
            {
                _logger.LogInformation($"User {dbUser.UserName} has been signed in");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(
                        new List<Claim>
                        {
                            new (ClaimsIdentity.DefaultNameClaimType, dbUser.UserName),
                            new (ClaimsIdentity.DefaultRoleClaimType, dbUser.Role)
                        },
                        "applicationCookie",
                        ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType))
                    );
            }
            else _logger.LogError($"Invalid login {login.UserName} or {login.Password}");

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [HttpGet, Authorize(Roles = "Admin")]
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}