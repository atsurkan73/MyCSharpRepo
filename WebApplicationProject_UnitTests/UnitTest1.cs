using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;
using WebApplicationProject;
using WebApplicationProject.Controllers;
using WebApplicationProject.Data;

namespace WebApplicationProject_UnitTests
{
    [TestFixture]
    public class Tests
        
    {
        Customer mockCustomer; // = new Customer();
        List<Customer> mockCustomers;
        List<ServiceProfile> mockServiceProfiles;
        ServiceProfile mockServiceProfile; // = new ServiceProfile();
        HomeController homeController;
        private readonly ILogger<HomeController> _logger;
        private readonly NewDbContext _dbContext;
        

        [SetUp]
        public void Setup()
        {
            mockCustomers = new List<Customer>();
            mockServiceProfiles = new List<ServiceProfile>();
            mockCustomer = new Customer( 2002, "testuser", "testuser@2002", "customer", "active", "Dave Seaman", "First street, 45", "+380950123456", 1002002, 0, 0, "");
            mockServiceProfile = new ServiceProfile (1002002, 2002, 0, "Base", "10.10.10.10", "no", "");

            //DbContextOptions<NewDbContext> options = new DbContextOptions<NewDbContext> ();
           // _dbContext = new NewDbContext(UseSqlServer(Settings.ConnectionString));

            homeController = new HomeController(_logger, _dbContext);

        }

        [Test]
        public async Task Test1()
       // public void Test1()
        {
            homeController.InputFormProfile(mockServiceProfile);
            var profile = await _dbContext.ServiceProfiles.FirstOrDefaultAsync(profile => profile.ProfileId == 1001);
            //Assert.Pass();
            Console.WriteLine($"Profile ID: {profile.ProfileId}");
            
            Assert.That(_dbContext.ServiceProfiles.FirstOrDefaultAsync(profile => profile.ProfileId == 2002), !Is.Null);
            //return await _dbContext.ServiceProfiles.FirstOrDefaultAsync(profile => profile.ProfileId == 2002) is not null;
        }
    }
}