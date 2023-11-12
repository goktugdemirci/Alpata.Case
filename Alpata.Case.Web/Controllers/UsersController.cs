using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Service.Contracts.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alpata.Case.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public Task<UserLoginResultDto> Authenticate(UserLoginDto userLoginDto)
        {
            return _userService.AuthenticateAsync(userLoginDto);
        }
        [HttpGet]
        public Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return _userService.GetAllAsync();
        }
        [HttpGet("{id}")]
        public Task<UserDto> GetByIdAsync(Guid id)
        {
            return _userService.GetByIdAsync(id);
        }
        [HttpPost]
        public Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
        {
            return _userService.AddAsync(userCreateDto);
        }
        [HttpPut]
        public Task UpdateAsync(UserUpdateDto userUpdateDto)
        {
            return _userService.UpdateAsync(userUpdateDto);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _userService.DeleteAsync(id);
        }
        [HttpGet("{email}/IsUniqueEmail")]
        public Task IsUniqueEmail(string email)
        {
            return _userService.IsEmailUniqueAsync(email);
        }
        [HttpGet("{phoneNumber}/IsUniquePhoneNumber")]
        public Task IsUniquePhoneNumber(string phoneNumber)
        {
            return _userService.IsPhoneNumberUniqueAsync(phoneNumber);
        }
        [HttpPost("ProfilePhoto/Upload")]
        public Task UploadProfilePhoto()
        {
            return _userService.UploadProfilePhoto(Request.Form);
        }
    }
}
