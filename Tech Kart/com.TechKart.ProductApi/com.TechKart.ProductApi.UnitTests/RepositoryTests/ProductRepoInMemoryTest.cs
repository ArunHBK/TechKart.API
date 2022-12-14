//using com.TechKart.ProductApi.Data;
//using com.TechKart.ProductApi.Models;
//using com.TechKart.ProductApi.Models.DTO;
//using com.TechKart.ProductApi.Repository;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Threading.Tasks;

//namespace com.TechKart.ProductApi.UnitTests.RepositoryTests
//{
//    [ExcludeFromCodeCoverage]
//    [TestFixture]
//    public class ProductRepoInMemoryTest
//    {
//        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
//           .UseInMemoryDatabase(databaseName: "TechKartDb")
//           .Options;

//        DataContext context;

//        [OneTimeSetUp]
//        public void Setup()
//        {
//            context = new DataContext(dbContextOptions);
//            context.Database.EnsureCreated();
//            SeedDatabase();

//          ProductRepo productRepo = new ProductRepo(context);
//        }
//        [OneTimeTearDown]
//        public void CleanUp()
//        {
//            context.Database.EnsureDeleted();
//        }

//        private void SeedDatabase()
//        {


//            var productDetails = new List<ProductDetail>()
//            {
//                new ProductDetail()
//                {
//                    Id=1, Name = "boAt Airdopes 141", Brand="boAt", Category="Tws", Description="Earbuds with 42H playtime,IPX4 Water Resistance",Price=899
//                },
//                new ProductDetail()
//                {
//                    Id=2, Name = "Samsung S20 FE", Brand="Samsung", Category="SmartPhone", Description="8GB RAM, 128 GB Storage,Android 11",Price=29999
//                },
//                new ProductDetail()
//                {
//                    Id=3, Name = "boAt BassHeads 100", Brand="boAt", Category="Wired Earphones", Description="In Ear wired earphones with Mic",Price=299
//                },
//                new ProductDetail()
//                {
//                    Id=4, Name = "Oneplus 10 pro", Brand="Oneplus", Category="SmartPhone", Description="12GB RAM,256 GB Storage, Android 12",Price=64999
//                }
//            };
//            context.ProductDetails.AddRange(productDetails);
//            context.SaveChanges();
//        }
//        [Test]
//        public async Task ProductRegistration_WithNewProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ProductAdd(new ProductDetailDto
//            {
//                Name = "Oneplus nord",
//                Brand="Oneplus",
//                Category="SmartPhone",
//                Description="12GB RAM,256 GB Storage, Android 12",
//                Price=64999
//            });

//            Assert.IsTrue(result.Status);
//        }
//        [Test]
//        public async Task ProductRegistration_WithExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ProductAdd(new ProductDetailDto
//            {
//                Name = "Oneplus 10 pro",
//                Brand="Oneplus",
//                Category="SmartPhone",
//                Description="12GB RAM,256 GB Storage, Android 12",
//                Price=64999
//            });

//            Assert.IsFalse(result.Status);
//        }
//        [Test]
//        public async Task ProductUpdate_WithExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ProductUpdate(4,new ProductDetailDto
//            {
//                Name = "Oneplus 10 pro",
//                Brand="Oneplus",
//                Category="SmartPhone",
//                Description="12GB RAM,256 GB Storage, Android 12",
//                Price=65999
//            });

//            Assert.IsTrue(result.Status);
//        }
//        [Test]
//        public async Task ProductUpdate_WithNonExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ProductUpdate(9, new ProductDetailDto
//            {
//                Name = "Oneplus 10 pro",
//                Brand="Oneplus",
//                Category="SmartPhone",
//                Description="12GB RAM,256 GB Storage, Android 12",
//                Price=65999
//            });

//            Assert.IsFalse(result.Status);
//        }
//        [Test]
//        public async Task ProductDelete_WithExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ProductDelete(2);
//            Assert.IsTrue(result.Status);
//        }
//        [Test]
//        public async Task ProductDelete_WithNonExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ProductDelete(7);
//            Assert.IsFalse(result.Status);
//        }
//        [Test]
//        public async Task ProductView_WithExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ViewProductDetails();
//            Assert.IsTrue(result.Status);
//        }
//        [Test]
//        public async Task ProductViewById_WithExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ViewProductById(1);
//            Assert.IsTrue(result.Status);
//        }
//        [Test]
//        public async Task ProductViewById_WithNonExistingProduct_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ViewProductById(10);
//            Assert.IsFalse(result.Status);
//        }
//        [Test]
//        public async Task ProductView_WithExistingProductPrice_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ViewByPrice(500,50000);
//            Assert.IsTrue(result.Status);
//        }
//        [Test]
//        public async Task ProductView_WithNoProductPrice_Test()
//        {

//            ProductRepo productRepo = new ProductRepo(context);
//            ResponseObject result = await productRepo.ViewByPrice(50, 200);
//            Assert.IsFalse(result.Status);
//        }
//        //[Test]
//        //public async Task ProductView_WithNoProductPrice_Test_Catch()
//        //{
//        //    var mock = new Mock<IProductRepo>();
//        //    int counter = 0;
//        //    mock.Setup(p => p.ViewProductDetails()).Callback(() => { if (counter++ >= 5) throw new Exception(); });
//        //    //  mock.Setup(p => p.ViewByPrice(6, 7))
//        //    //  .ThrowsAsync(new Exception());
//        //    //.Callback(() => throw new Exception());

//        //    ProductRepo productRepo = new ProductRepo(context);
//        //    ResponseObject result = await productRepo.ViewProductDetails();
//        //    Assert.IsFalse(result.Status);
//        //}
//    }
//}