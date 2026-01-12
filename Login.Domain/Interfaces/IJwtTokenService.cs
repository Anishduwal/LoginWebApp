using Login.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
