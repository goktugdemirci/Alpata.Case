using Alpata.Case.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Alpata.Case.Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [Encrypted]
        public string Email { get; set; }
        [Encrypted]
        public string PhoneNumber { get; set; }
        [Encrypted]
        public string Password { get; set; }
        
        public string? ProfilePicture { get; set; }
    }
}
