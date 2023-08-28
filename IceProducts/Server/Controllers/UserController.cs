using FluentValidation;
using IceProducts.Server.Data;
using IceProducts.Server.Services;
using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.InputModels;
using IceProducts.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IceProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IValidator<ChangePasswordInputModel> _validator;

        public UserController(IUserService userService, ITokenHandler tokenHandler, IValidator<ChangePasswordInputModel> validator)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
            _validator = validator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessToken>> Login([FromBody] UserInputModel userInput)
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
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordInputModel changePasswordInputModel)
        {
            //validate the new password later
            var validationResult = await _validator.ValidateAsync(changePasswordInputModel);

            if (validationResult.IsValid)
            {
                //no need to use HttpContextAccessor there's only one record
                var user = await _userService.GetFirst();
                _userService.UpdatePassword(user, changePasswordInputModel.NewPassword);
                await _userService.Save();
                return Ok();
            }
            var errorMessages = string.Join(Environment.NewLine, validationResult.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
            return BadRequest(new BaseResponse(false, errorMessages));
        }


    }
}
