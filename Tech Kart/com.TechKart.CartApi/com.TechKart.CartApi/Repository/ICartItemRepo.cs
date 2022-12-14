using com.TechKart.CartApi.Models;
using com.TechKart.CartApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace com.TechKart.CartApi.Repository
{
    public interface ICartItemRepo
    {
        public Task<ResponseObject> CartItemAdd(int productId);
        public Task<ResponseObject> CartItemsDelete(int Id);


    }
}
