using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserService
    {
        public Service Create(User user);
        public Service Update(User user);
        public Service Delete(int id);
        public IQueryable<UserModel> Query();
    }
    public class UserService : Service, IUserService
    {
        public UserService(DB db) : base(db)
        {
        }

        public Service Create(User user)
        {
            throw new NotImplementedException();
        }

        public Service Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).Where(u => u.IsActive).Select(u => new UserModel() { Record = u });
        }

        public Service Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
