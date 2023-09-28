using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> GetUser()
        {
            var user = _context.Users
                .Include(u => u.Orders)
                .OrderByDescending(u => u.Orders.Count())
                .FirstOrDefaultAsync();
            return user;
        }

        public Task<List<User>> GetUsers()
        {
            var inactiveUsers = _context.Users
                .Where(u => u.Status == UserStatus.Inactive)
                .ToListAsync();
            return inactiveUsers;
        }
    }
}