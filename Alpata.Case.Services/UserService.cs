using Alpata.Case.Domain.Models;
using Alpata.Case.Domain.Repository;
using Alpata.Case.Domain.Shared.Current;
using Alpata.Case.Domain.Shared.Descriptions;
using Alpata.Case.Service.Contracts.Common;
using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Service.Contracts.Dtos.User;
using Alpata.Case.Services.Base;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alpata.Case.Services
{
    public class UserService : BaseApplicationService<User, UserDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        private readonly AlpataCurrentUser _alpataCurrentUser;

        public UserService(IRepository<User> repository, IMapper mapper, IConfiguration configuration, IFileService fileService, AlpataCurrentUser alpataCurrentUser) : base(repository, mapper)
        {
            _configuration = configuration;
            _fileService = fileService;
            _alpataCurrentUser = alpataCurrentUser;
        }
        public override async Task UpdateAsync(UserUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            var mappedEntity = _mapper.Map<User>(dto);
            mappedEntity.Password = entity.Password;
            await _repository.UpdateAsync(mappedEntity);
        }
        public override async Task<UserDto> AddAsync(UserCreateDto input)
        {
            await IsEmailUniqueAsync(input.Email);
            await IsPhoneNumberUniqueAsync(input.PhoneNumber);
            return await base.AddAsync(input);
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

        public async Task<UserDto> GetCurrentUserAsync()
        {
            Guid currentUserId = Guid.Parse(_alpataCurrentUser.Id.Value.ToString());
            var query = _repository.GetQueryable();
            var user = await query.FirstOrDefaultAsync(x => x.Id == currentUserId);
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task IsEmailUniqueAsync(string email)
        {

            bool exists = await _repository.GetQueryable().AnyAsync(x => x.Email == email);
            if (exists)
            {
                throw new Exception($"{email}\nThis email address is already on use.");
            }
        }

        public async Task IsPhoneNumberUniqueAsync(string phoneNumber)
        {
            bool exists = await _repository.GetQueryable().AnyAsync(x => x.PhoneNumber == phoneNumber);
            if (exists)
            {
                throw new Exception($"{phoneNumber}\nThis phone number is already on use.");
            }
        }

        public async Task UploadProfilePhoto(IFormCollection form)
        {
            var result = Guid.TryParse(form["userId"].ToString(), out Guid userId);
            if (!result) throw new Exception("User not found");

            User user = await _repository.GetByIdAsync(userId);
            IFormFile file = form.Files[0];
            user.ProfilePicture = await _fileService.CopyFileToDirectory(file);

            await _repository.UpdateAsync(user);
        }
    }
}
