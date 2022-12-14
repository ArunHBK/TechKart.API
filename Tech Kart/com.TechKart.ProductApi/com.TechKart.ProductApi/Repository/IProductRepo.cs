using com.TechKart.ProductApi.Models.DTO;
using com.TechKart.ProductApi.Models;
using System.Threading.Tasks;

namespace com.TechKart.ProductApi.Repository
{
    public interface IProductRepo
    {
        public Task<ResponseObject> ViewProductDetails();
        public Task<ResponseObject> ViewProductById(int id);
        public Task<ResponseObject> ViewByPrice(double priceFrom, double priceTo);
        public Task<ResponseObject> ProductAdd(ProductDetailDto product);
        public Task<ResponseObject> ProductUpdate(int Id, ProductDetailDto product);
        public Task<ResponseObject> ProductDelete(int Id);
    }
}
