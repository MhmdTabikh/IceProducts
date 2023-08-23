using IceProducts.Server.Data;
using IceProducts.Server.Services;
using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IceProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationController(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public async Task<ActionResult<AccessToken>> Login([FromForm] UserInputModel userInput)
        {
            var user = await _userService.IsAuthenticated(userInput);

            if(user != null)
            {
                return Ok(_tokenHandler.CreateAccessToken(user));
            }

            return Unauthorized(new BaseResponse(false, "Wrong email or password"));
        }
    }
}
