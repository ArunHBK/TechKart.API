using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechKartApplication.Models;
using TechKartApplication.Models.DTO;
using TechKartApplication.Repository;

namespace TechKartApplication.Controllers
{
    [Route("api/v1.0/techkart/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]

    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        
        [HttpPost]
        [Route("Login"),AllowAnonymous]
        public async Task<ActionResult<ResponseObject>> Authentication(LoginDto user)
        {
            ResponseObject result = await _userRepo.Login(user);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        [Route("UserRegister"),AllowAnonymous]
        public async Task<ActionResult<ResponseObject>> Registration(UserDetailDto user)
        {
            ResponseObject result = await _userRepo.UserRegistration(user);
            return StatusCode(result.StatusCode, result);
        }
        
    }
}
