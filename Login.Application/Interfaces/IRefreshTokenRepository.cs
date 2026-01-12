using Login.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetAsync(string token);
        Task SaveAsync(RefreshToken refreshToken);
        Task RevokeAsync(string token);
    }

}
