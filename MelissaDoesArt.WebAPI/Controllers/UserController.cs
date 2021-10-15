using MelissaDoesArt.Infrastructure.Interfaces;
using MelissaDoesArt.Infrastructure.Models;
using MelissaDoesArt.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;

namespace MelissaDoesArt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ClaimsPrincipal _caller;
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            this.unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User user)
        {
            //var users = await unitOfWork.Users.GetAll();
            //var email = users.SingleOrDefault(x => x.Email == user.Email);

            var users = await unitOfWork.Users.GetAll();
            var password = users.FirstOrDefault(x => x.Password == user.Password);
            var userData = users.FirstOrDefault(x => x.Email == user.Email);
            // verify password
            if (userData == null || !BCrypt.Net.BCrypt.Verify(user.Password,userData.Password))
                return Unauthorized(new { message = "Username or password is incorrect" });

            // authentication successful
            var token = TokenService.CreateToken(userData);
            userData.Password = "";
            return new
            {
                user = userData,
                token = token
            };

            //if (userData == null)
            //    return NotFound(new { message = "Your Email or Password is invalid" });

            //var token = TokenService.CreateToken(userData);
            //userData.Password = "";
            //return new
            //{
            //    user = userData,
            //    token = token
            //};
        }


        [HttpPost]
        public async Task<ActionResult<int>> Create(User user)
        {
            var data = await unitOfWork.Users.Create(user);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Users.GetAll();
            return Ok(data);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUser()
        //{
        //    var data = await unitOfWork.Users.GetUser();
        //    return Ok(data);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Users.GetById(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            var data = await unitOfWork.Users.Update(user);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Users.Delete(id);
            return Ok(data);
        }
    }
}
