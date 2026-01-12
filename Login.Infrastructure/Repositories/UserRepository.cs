using Login.Application.Interfaces;
using Login.Domain.Entities;
using Login.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<int> RegisterAsync(string email, string hashPassword)
        {
            try
            {
                var UserData = new User
                {
                    Email = email,
                    PasswordHash = hashPassword
                };
                await _context.User.AddAsync(UserData);
                await _context.SaveChangesAsync();

                return UserData.Id;
            }
            catch(DbException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
