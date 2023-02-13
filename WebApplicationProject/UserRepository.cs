using WebApplicationProject.Data;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;


namespace WebApplicationProject
{
    public class UserRepository
    {

        private readonly NewDbContext _context;
        private readonly ILogger<HomeController> _logger;
        
        public UserRepository(NewDbContext context) => _context = context;

        public Task<List<Customer>> GetUsers() => _context.Customers.ToListAsync();

        public async Task<List<Customer>> GetCustomers()
        {
            var LogUsers = await _context.Customers.Where(customer => customer.Id > 0).ToListAsync();
            
            foreach (var user in LogUsers)
            {
                user.Login = user.Login.Trim();
                user.Password = user.Password.Trim();
                user.Role = user.Role.Trim();
                Console.WriteLine($"{user.Id} - {user.Login} - {user.ServiceProfileId}");
            }
            return LogUsers;
        }

        public async Task<List<ServiceProfile>> GetProfiles()
        {
            var profiles = await _context.ServiceProfiles.ToListAsync();
            foreach (var profile in profiles)
            {
                Console.WriteLine($"{profile.ProfileId} - {profile.UserId} - {profile.IPaddress}");
            }
            return profiles;
        }

        public async Task<List<TariffPlan>> GetTariffs()
        {
            var tariffs = await _context.TariffPlans.ToListAsync();
            foreach (var tariff in tariffs)
            {
                Console.WriteLine($"{tariff.TariffId} - {tariff.TariffName}");
            }
            return tariffs;
        }
    }
}