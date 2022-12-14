using System.Collections.Generic;
using System.Threading.Tasks;
using TechKartApplication.Models;
using TechKartApplication.Models.DTO;

namespace TechKartApplication.Repository
{
    public interface IUserRepo
    {
        //public Task<List<UserDetail>> ViewUserDetails();
        //public Task<List<LoginDetail>> ViewLoginDetails();
        public Task<ResponseObject> Login(LoginDto user);
        public Task<ResponseObject> UserRegistration(UserDetailDto user);
       // public Task<bool> UserDetailsUpdate(int Id, UserDetailDto user);
       // public Task<bool> AccountDelete(string Username);
    }
}
