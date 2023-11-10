using Alpata.Case.Domain.Models;
using Alpata.Case.Domain.Repository;
using Alpata.Case.Domain.Shared.Descriptions;
using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Service.Contracts.Dtos.User;
using Alpata.Case.Services.Base;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alpata.Case.Services
{
    public class UserService : BaseApplicationService<User, UserDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IConfiguration _configuration;
        public UserService(IRepository<User> repository, IMapper mapper, IConfiguration configuration) : base(repository, mapper)
        {
            _configuration = configuration;
        }

        public async Task<UserLoginResultDto> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var query = _repository.GetQueryable();
            var user = await query.FirstOrDefaultAsync(x => x.Email == userLoginDto.Email && x.Password == userLoginDto.Password);
            if (user == null)
                throw new Exception("Invalid credentials");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:PrivateKey"]);
            int expireHour = Convert.ToInt32(_configuration["JwtSettings:ExpireHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimNames.Name.ToString(),user.Name),
                    new Claim(JwtClaimNames.Surname.ToString(),user.Surname),
                    new Claim(JwtClaimNames.FullName.ToString(),$"{user.Name} {user.Surname}"),
                    new Claim(JwtClaimNames.Email.ToString(),user.Email),
                    new Claim(JwtClaimNames.PhoneNumber.ToString(),user.PhoneNumber),
                    new Claim(JwtClaimNames.Id.ToString(),user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(expireHour),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string result = tokenHandler.WriteToken(token);
            UserLoginResultDto userLoginResult = new UserLoginResultDto(result);
            return userLoginResult;
        }

        public Task<UserDto> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task IsEmailUniqueAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task IsPhoneNumberUniqueAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
