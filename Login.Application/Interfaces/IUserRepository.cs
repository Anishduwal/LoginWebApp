using Login.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<int> RegisterAsync(string email, string hashPassword);
        Task<User?> GetByIdAsync(int id);
    }
}
