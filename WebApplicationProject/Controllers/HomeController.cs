using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using WebApplicationProject.Models;
using WebApplicationProject.Data;

namespace WebApplicationProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewDbContext _dbContext;
        public bool isAuthintificated = true;

        public List<Customer> LogUsers = new List<Customer>();

        public Customer LoggedCustomer { get; set; }



        public HomeController(ILogger<HomeController> logger, NewDbContext context)
        {
            _logger = logger;
            _dbContext = context;

        }

        public async Task<List<Customer>> GetCustomers()
        {


            LogUsers = await _dbContext.Customers.Where(customer => customer.Id > 0).ToListAsync();
            foreach (var user in LogUsers)
            {
                user.Login = user.Login.Trim();
                user.Password = user.Password.Trim();
                user.Role = user.Role.Trim();
                Console.WriteLine($"{user.Id} - {user.Login} - {user.Role}"); }
            return LogUsers;
        }

        //[HttpGet, Authorize(Roles = "admin")]
        [HttpGet]

        //public async Task IndexAuth() => await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            Customer customer = await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == Id);

            if (customer == null)
            {
                //     Console.WriteLine($"{customer.Id} - {customer.Login} - {customer.Role}");
                _logger.LogError("Error while deleting customer == null");
            }
            else if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<ActionResult<List<Customer>>> Index() => View(await _dbContext.Customers.ToListAsync());


        public ActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(Customer customer)
        {
            LogUsers = await GetCustomers();
            LoggedCustomer = LogUsers
               .FirstOrDefault(user => user.Login == customer.Login.Trim()
                && user.Password.Trim() == customer.Password);

            if (LoggedCustomer is not null)
            {
                _logger.LogInformation($"User login = {LoggedCustomer.Login} has been signed in with role  = {LoggedCustomer.Role}");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(
                        new List<Claim>
                        {
                            new (ClaimsIdentity.DefaultNameClaimType, LoggedCustomer.Login),
                            new (ClaimsIdentity.DefaultRoleClaimType, LoggedCustomer.Role)
                        },
                        "applicationCookie",
                        ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType))
                    );
                isAuthintificated = true;
                return RedirectToAction("Index");
            }
            else _logger.LogError($"Invalid login {customer.Login} or {customer.Password}");
            return RedirectToAction("Login");

        }


        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            isAuthintificated = false;
            return RedirectToAction("Index");
        }




        //[HttpGet, Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<Customer>> InputForm(int id) =>
         View(await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id));


        [HttpPost]
        public async Task<IActionResult> InputForm(Customer customer)
        {
            //if (!customer.Id.Equals(_dbContext.Customers.FirstOrDefaultAsync()))
            //{
            _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            //}
            //else { Console.WriteLine("Use another customer Id"); }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult<TariffPlan>> EditCustomer(int id)
        {
            if (!User.Identity!.IsAuthenticated || !User.IsInRole("admin")) return RedirectToAction("Login");
            return View(await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            _dbContext.Entry(await _dbContext.Customers
                    .FirstOrDefaultAsync(currCustomer => currCustomer.Id == customer.Id))
                .CurrentValues.SetValues(customer);
            Console.WriteLine($"{customer.Id} - {customer.Login} - {customer.Name}");
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult<List<TariffPlan>>> TariffPlans()
        {
            return View(await _dbContext.TariffPlans.ToListAsync());
        }

        public async Task<ActionResult<TariffPlan>> InputFormTariff(string name) =>
            View(await _dbContext.TariffPlans.FirstOrDefaultAsync(tariff => tariff.TariffName.Equals(name)));

        //[HttpPost, Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> InputFormTariff(TariffPlan tariff)
        {
            if (_dbContext.TariffPlans.FirstOrDefault(tarif => tarif.TariffName.Equals(tariff.TariffName)) is null)
            {
                Console.WriteLine($"{tariff.TariffId} - {tariff.TariffName}");
                _dbContext.TariffPlans.AddAsync(tariff);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("TariffPlans");
            }
            else
            {
                _logger.LogError("Use another Tariff name");
                return RedirectToAction("InputFormTariff"); 
            }
        }

        //[HttpGet, Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<TariffPlan>> EditTariff(int id)
        {
            if (!User.Identity!.IsAuthenticated || !User.IsInRole("admin")) return RedirectToAction("Login");
            return View(await _dbContext.TariffPlans.FirstOrDefaultAsync(tariff => tariff.TariffId == id));
        }

        //[HttpPost, Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditTariff(TariffPlan tariffPlan)
        {
            _dbContext.Entry(await _dbContext.TariffPlans
                    .FirstOrDefaultAsync(tariff => tariff.TariffName.Equals(tariffPlan.TariffName)))
                .CurrentValues.SetValues(tariffPlan);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("TariffPlans");
        }

        public async Task<IActionResult> DeleteTariffPlan(int id)
        {
            if (_dbContext.TariffPlans.FirstOrDefault(tarif => tarif.TariffId == id) is not null)
            {
                TariffPlan tariff = await _dbContext.TariffPlans.FirstOrDefaultAsync(tarif => tarif.TariffId == id);

                _dbContext.TariffPlans.Remove(tariff);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("TariffPlans");
            }
            else
            {
                _logger.LogError("Error while deleting. Tariff is not found.");
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceProfile>>> ServiceProfiles()
        {
            return View(await _dbContext.ServiceProfiles.ToListAsync());
        }

        //[HttpPost, Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ServiceProfiles(ServiceProfile serviceProfile)
        {
            _dbContext.Entry(await _dbContext.ServiceProfiles
                    .FirstOrDefaultAsync(profile => profile.ProfileId == serviceProfile.ProfileId))
                .CurrentValues.SetValues(serviceProfile);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"{serviceProfile.ProfileId} - {serviceProfile.UserId}");
            return RedirectToAction("ServiceProfiles");
        }

        [HttpGet]
        public async Task<ActionResult<TariffPlan>> InputFormProfile(int id)
        {
           
            return View(await _dbContext.ServiceProfiles.FirstOrDefaultAsync(profile => profile.ProfileId == id));

        }
        //[HttpPost, Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> InputFormProfile(ServiceProfile serviceProfile)
        {
           // var serviceProfiles = _dbContext.ServiceProfiles;

            //if (!serviceProfile.ProfileId.Equals(_dbContext.ServiceProfiles.FirstOrDefaultAsync()))
            if (_dbContext.ServiceProfiles.FirstOrDefault(profile => profile.ProfileId == serviceProfile.ProfileId) is null)
                {
                _dbContext.ServiceProfiles.AddAsync(serviceProfile);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("ServiceProfiles");
            }
            else {
                _logger.LogError("Error while adding Service profile. Profile Id is already exists");
                return RedirectToAction("InputFormProfile");
            }

        }

        //[HttpGet, Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<ServiceProfile>> EditServiceProfile(int id)
        {
            if (!User.Identity!.IsAuthenticated || !User.IsInRole("admin")) return RedirectToAction("Login");
            return View(await _dbContext.ServiceProfiles.FirstOrDefaultAsync(profile => profile.ProfileId == id));
        }
        //[HttpPost, Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditServiceProfile(ServiceProfile serviceProfile)
        {
            _dbContext.Entry(await _dbContext.ServiceProfiles
                    .FirstOrDefaultAsync(profile => profile.ProfileId == serviceProfile.ProfileId))
                .CurrentValues.SetValues(serviceProfile);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("ServiceProfiles");
        }

        public async Task<IActionResult> DeleteServiceProfile(int id)
        {
            if (_dbContext.ServiceProfiles.FirstOrDefault(profile => profile.ProfileId == id) is not null)
            {
                var profile = await _dbContext.ServiceProfiles.FirstOrDefaultAsync(profile => profile.ProfileId == id);

                _dbContext.ServiceProfiles.Remove(profile);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("ServiceProfiles");
            }
            else
            {
                _logger.LogError("Error while deleting. Service profile is not found");
                return View();
            }
        }

            [HttpGet]
        public async Task<ActionResult<Customer>> UpdatePassword(int Id) 
        {
            //Console.WriteLine($"Update password for : {Id}");
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == Id);
            Console.WriteLine($"Going to change password for customer = {customer.Login}");
            return View(customer);
        }

        //[HttpPost, Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword (Customer customerPassword)
        {
            //var currentCustomer = await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == customerPassword.Id);
            //currentCustomer.Password = newPassword;
            var customerEntry = await _dbContext.Customers
                     .FirstOrDefaultAsync(customer => customer.Id == customerPassword.Id);
            Console.WriteLine($"customer.Login = {customerEntry.Login}, Pasword: {customerEntry.Password}");
            _dbContext.Entry(await _dbContext.Customers
                     .FirstOrDefaultAsync(customer => customer.Id == customerPassword.Id))
                .CurrentValues. SetValues(customerPassword);
            Console.WriteLine($"New Password = {customerEntry.Password}");
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