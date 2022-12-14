using com.TechKart.CartApi.Data;
using com.TechKart.CartApi.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace com.TechKart.CartApi.Repository
{
    public class CartRepo:ICartRepo
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepo(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor=httpContextAccessor;
        }
        ResponseObject response = new ResponseObject();
        public async Task<ResponseObject> ViewCartById()
        {
            try
            {
                int LoginId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("Id")); ;
                var cart = await _context.Carts.Where(m => m.LoginId == LoginId)
                    .Include(c =>c.cartItems)
                    .FirstOrDefaultAsync();

                var cartItems = await _context.CartItems.Where(m => m.CartId==cart.CartId)
                    .Include(c => c.product).ToListAsync();
               
                    cart.cartItems=cartItems;
                    double totalCartPrice = 0;
                    foreach(var cartItem in cartItems)
                    {
                        totalCartPrice += cartItem.Item_Total;
                    }
                    cart.GrandTotal = totalCartPrice;

                     _context.Carts.Update(cart);
                     await  _context.SaveChangesAsync();

                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Value = cart;
                    response.Message = "Cart Items";
                    return response;
               
            }
            catch (Exception ex)
            {
                response.Status=false;
                response.StatusCode= StatusCodes.Status500InternalServerError;
                response.Message=ex.Message;
                return response;
            }
        }

        public async Task<ResponseObject> CartAdd()
        {
            try
            {
                int LoginId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("Id")); ;
                var UserCheck = await _context.LoginDetails.Where(m => m.LoginId == LoginId).FirstOrDefaultAsync();

                if (UserCheck != null)
                {
                    if (!_context.Carts.Any(a => a.LoginId==LoginId))
                    {
                        Cart mapping = new Cart
                        {
                            GrandTotal=0,
                            Created_at=DateTime.Now,
                            Modified_at=DateTime.Now,
                            LoginDetail=UserCheck
                        };

                        _context.Carts.Add(mapping);
                        await _context.SaveChangesAsync();
                        response.Status=true;
                        response.StatusCode= StatusCodes.Status200OK;
                        response.Message = "Cart added successfully";
                        return response;
                    }
                    else
                    {
                        response.Status=false;
                        response.StatusCode= StatusCodes.Status400BadRequest;
                        response.Message ="Already a cart has been created for this account";
                        return response;
                    }
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message ="User not found to create cart";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Status=false;
                response.StatusCode= StatusCodes.Status500InternalServerError;
                response.Message=ex.Message;
                return response;
            }
        }

        public async Task<ResponseObject> EmptyCart()
        {
            try
            {
                int LoginId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("Id")); ;
                var CartId = await _context.Carts.Where(m => m.LoginId == LoginId ).FirstOrDefaultAsync();
                 
                var cartItemsOfUser = await _context.CartItems.Where(m => m.CartId == CartId.CartId).ToListAsync();
                if (cartItemsOfUser != null)
                {
                    foreach(var cartItem in cartItemsOfUser)
                    {
                        _context.CartItems.Remove(cartItem);
                    }
                    await _context.SaveChangesAsync();
                    response.Status=true;
                    response.StatusCode= StatusCodes.Status200OK;
                    response.Message = "All cart item has been deleted successfully";
                    return response;
                }
                else
                {
                    response.Status=false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message ="No Cart Items to delete";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Status=false;
                response.StatusCode= StatusCodes.Status500InternalServerError;
                response.Message=ex.Message;
                return response;
            }
        }
    }
}
