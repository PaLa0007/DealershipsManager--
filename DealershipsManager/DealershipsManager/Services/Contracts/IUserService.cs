using DealershipsManager.Data.Entities;
using DealershipsManager.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DealershipsManager.Services.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserById(string id);

        IQueryable<User> GetAllUsers();

        Task<List<User>> GetAllUsersAsync(Expression<Func<User, bool>> predicate);

        Task<bool> AddUser(RegisterUserViewModel entity);

        Task<IdentityResult> UpdateUser(DetailsUserViewModel entity);

        Task<IdentityResult> RemoveUser(string id);

        Task<bool> Login(LoginUserViewModel entity);

        void Logout();
    }
}
