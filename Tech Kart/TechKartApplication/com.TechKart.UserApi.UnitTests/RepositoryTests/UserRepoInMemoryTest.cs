using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TechKartApplication.Data;
using TechKartApplication.Models;
using TechKartApplication.Models.DTO;
using TechKartApplication.Repository;

namespace com.TechKart.UserApi.UnitTests.RepositoryTests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UserRepoInMemoryTest
    {
        Mock<IConfiguration> configuration = new Mock<IConfiguration>();

        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TechKartDb")
            .Options;

        DataContext context;
        UserRepo userRepo;

        [OneTimeSetUp]
        public void Setup()
        { 
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            userRepo = new UserRepo(context, configuration.Object);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {


            var userDetails = new List<UserDetail>()
            {
                new UserDetail()
                {
                   Id = 1,FirstName="Vaish", LastName="Tammy", Age=30,Gender="Female", Username ="VaishTammy",ContactNumber=789700986,Address="Avadi"
                },
                new UserDetail()
                {
                   Id = 2,FirstName="Arun", LastName="HBK", Age=24,Gender="Male", Username ="ArunHBK",ContactNumber=4974943749,Address="Manali"
                },
                new UserDetail()
                {
                   Id = 3,FirstName="Jayarath", LastName="Celine", Age=24,Gender="Male", Username ="JayarathCeline",ContactNumber=996786974,Address="Siruseri"
                },
            };
            context.UserDetails.AddRange(userDetails);
            var loginDetails = new List<LoginDetail>()
            {
                new LoginDetail()
                {
                    LoginId = 1, Username ="VaishTammy", Password="12345", Role="User"
                },
                new LoginDetail()
                {
                   LoginId = 2, Username ="ArunHBK", Password ="12345", Role="User"
                },
                new LoginDetail()
                {
                   LoginId = 3, Username ="JayarathCeline", Password ="12345", Role="User"
                },
            };
            context.LoginDetails.AddRange(loginDetails);

            context.SaveChanges();
        }
        [Test]
        public async Task UserLogin_Success_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("TechKartApplicationSecurityCode");
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.Login(new LoginDto { Username="VaishTammy", Password="12345" });

            Assert.IsTrue(result.Status);
        }
        [Test]
        public async Task UserLogin_IncorrectCredential_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("TechKartApplicationSecurityCode");
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.Login(new LoginDto { Username="Thanya", Password="12345" });

            Assert.IsFalse(result.Status);
        }
        [Test]
        public async Task UserLogin_WithException_test()
        {
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("TechKartApplicationSecurityCode");
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.Login(new LoginDto { Username="Thanya", Password="12345" });

            Assert.IsFalse(result.Status);
        }
        [Test]
        public async Task UserRegistration_WithNewUsername_Test()
        {
            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.UserRegistration(new UserDetailDto
            {
                FirstName="Thanya",
                LastName="Godwin",
                Age=20,
                Gender="Female",
                Username ="ThanyaGodwin",
                ContactNumber=4549959,
                Address="Coimbatore"
            });

            Assert.IsTrue(result.Status);
        }
        [Test]
        public async Task UserRegistration_WithExistingUsername_Test()
        {

            UserRepo userRepo = new UserRepo(context, configuration.Object);
            ResponseObject result = await userRepo.UserRegistration(new UserDetailDto
            {
                FirstName="Vaish",
                LastName="Tammy",
                Age=30,
                Gender="Female",
                Username ="VaishTammy",
                ContactNumber=789700986,
                Address="Avadi"

            });

            Assert.IsFalse(result.Status);
        }
    }
}
