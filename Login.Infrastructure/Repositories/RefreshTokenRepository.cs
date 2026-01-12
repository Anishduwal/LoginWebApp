using Login.Application.Interfaces;
using Login.Domain.Entities;
using Login.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Login.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetAsync(string token)
        {
            return await _context.LoginTokens
                .FirstOrDefaultAsync(x => x.Token == token && !x.IsRevoked);
        }

        public async Task SaveAsync(RefreshToken refreshToken)
        {
            _context.LoginTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAsync(string token)
        {
            var rt = await _context.LoginTokens.FirstOrDefaultAsync(x => x.Token == token);
            if (rt != null)
            {
                rt.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }

}
