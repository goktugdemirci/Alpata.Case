using Alpata.Case.Domain.Shared.Descriptions;
using Microsoft.AspNetCore.Http;

namespace Alpata.Case.Domain.Shared.Current
{
    public class AlpataCurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AlpataCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsAuthenticated => Guid.TryParse(GetTokenPropertyValue(JwtClaimNames.Id), out Guid result);
        public Guid? Id => GetTokenPropertyValue(JwtClaimNames.Id) == null ? null : Guid.Parse(GetTokenPropertyValue(JwtClaimNames.Id));
        public string UserName => GetTokenPropertyValue(JwtClaimNames.Email);
        public string Name => GetTokenPropertyValue(JwtClaimNames.Name);
        public string SurName => GetTokenPropertyValue(JwtClaimNames.Surname);
        public string FullName => $"{this.Name} {this.SurName}";
        public string PhoneNumber => GetTokenPropertyValue(JwtClaimNames.PhoneNumber);
        public string Email => GetTokenPropertyValue(JwtClaimNames.Email);
        private string GetTokenPropertyValue(JwtClaimNames propertyName)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claim = claims?.Where(x => x?.Type == propertyName.ToString());
            var value = claim?.FirstOrDefault()?.Value;
            return value;
        }
    }
}
