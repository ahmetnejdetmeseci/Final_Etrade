using Business.Models;
using Business.Services.Bases;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly Db _db;

       
        public UserService(Db db)
        {
            _db = db;
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive)
                .ThenBy(u => u.UserName)
                .Select(e => new UserModel()
                {
                    Id = e.Id,
                    
                    Password = e.Password,
                    RoleId = e.RoleId,
                    
                    UserName = e.UserName,
                    IsActive = e.IsActive,
                    RoleOutput = e.Role.Name
                    
                });
        }
    }
}
