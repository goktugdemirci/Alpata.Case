using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Service.Contracts.Dtos.User
{
    public class UserLoginResultDto
    {
        public UserLoginResultDto(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
    }
}
