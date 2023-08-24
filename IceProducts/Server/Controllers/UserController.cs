using IceProducts.Server.Data;
using IceProducts.Server.Services;
using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IceProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public UserController(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public async Task<ActionResult<AccessToken>> Login([FromForm] UserInputModel userInput)
        {
            var user = await _userService.Authorize(userInput);

            if(user != null)
            {
                return Ok(_tokenHandler.CreateAccessToken(user));
            }

            return Unauthorized(new BaseResponse(false, "Wrong email or password"));
        }

        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordInputModel changePasswordInputModel)
        {
            //validate the new password later

            //no need to use HttpContextAccessor there's only one record
            var user = await _userService.GetFirst();
            _userService.UpdatePassword(user, changePasswordInputModel.NewPassword);
            await _userService.Save();
            return Ok();
        }


    }
}
