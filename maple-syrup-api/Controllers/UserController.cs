using maple_syrup_api.CustomAttributes;
using maple_syrup_api.Dto.Auth;
using maple_syrup_api.Dto.User;
using maple_syrup_api.Models;
using maple_syrup_api.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService pUserService)
        {
            _userService = pUserService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginOut>> Login(LoginIn pInput)
        {
            UserSummary user = await _userService.Login(pInput.DiscordCode);
            return Ok(new LoginOut() {
             Id = user.Id,
             Image = user.Image,
             AccessToken = user.AccessToken,
             Username = user.Username
            });
        }

        [HttpGet]
        public ActionResult<GetUserPlayerListOut> GetUserPlayerList(GetUserPlayerListIn pInput)
        {
            GetUserPlayerListOut result = new GetUserPlayerListOut()
            {
                playerList = _userService.GetUserPlayerList(pInput.UserId)
            };
            return Ok(result);
        }



        [HttpPost]
        [Authorize]
        public ActionResult<UserSummary> Authenticate()
        {
            User user = (User)HttpContext.Items["User"];
            string token = HttpContext.Items["Token"].ToString();
            UserSummary userSummary = new UserSummary()
            {
                Username = user.UserName,
                AccessToken = token,
                Id = user.Id,
                Image = user.Image
            };
            return Ok(userSummary);
        }

    }
}
