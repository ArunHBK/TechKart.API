using com.TechKart.CartApi.Models;
using System.Threading.Tasks;

namespace com.TechKart.CartApi.Repository
{
    public interface ICartRepo
    {
        public Task<ResponseObject> CartAdd();
        public Task<ResponseObject> ViewCartById();
        public Task<ResponseObject> EmptyCart();

    }
}
