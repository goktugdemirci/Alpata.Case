using Alpata.Case.Service.Contracts.Contracts.Base;
using Alpata.Case.Service.Contracts.Dtos.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Service.Contracts.Contracts
{
    public interface IUserService : IBaseApplicationService<UserDto,UserCreateDto,UserUpdateDto>
    {
        public Task IsEmailUniqueAsync(string email);
        public Task IsPhoneNumberUniqueAsync(string phoneNumber);
        public Task<UserLoginResultDto> AuthenticateAsync(UserLoginDto userLoginDto);
        public Task<UserDto> GetCurrentUserAsync();
        public Task UploadProfilePhoto(IFormCollection form);
    }
}
