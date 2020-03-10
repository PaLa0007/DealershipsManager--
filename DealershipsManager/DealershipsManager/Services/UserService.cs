using DealershipsManager.Data;
using DealershipsManager.Data.Entities;
using DealershipsManager.Initialisation;
using DealershipsManager.Models.User;
using DealershipsManager.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DealershipsManager.Services
{
    public class UserService : IUserService
    {

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly DealershipsManagerDbContext dbContext;

        public UserService(SignInManager<User> signInManager, UserManager<User> userManager, DealershipsManagerDbContext dbContext)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<User> GetUserById(string id)
        {
            return await this.userManager.FindByIdAsync(id);
        }

        public IQueryable<User> GetAllUsers()
        {
            return this.userManager.Users.AsQueryable();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await GetAllUsers().ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync(Expression<Func<User, bool>> predicate)
        {
            return await GetAllUsers().Where(predicate).ToListAsync();
        }

        public async Task<bool> AddUser(RegisterUserViewModel model)
        {
            if (model.Username == null ||
                model.Password == null ||
                model.ConfirmPassword == null ||
                model.Email == null ||
                model.FirstName == null ||
                model.MiddleName == null ||
                model.LastName == null ||
                model.PersonalNumber == null ||
                model.Address == null ||
                model.PhoneNumber == null)
            {
                return false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                return false;
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                PersonalNumber = model.PersonalNumber,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                IsAdministrator = model.IsAdministrator
            };

            var userCreateResult = await this.userManager.CreateAsync(user, model.Password);

            if (!userCreateResult.Succeeded)
            {
                return false;
            }

            IdentityResult addRoleResult;


            if (model.IsAdministrator == true)
            {
                addRoleResult = await this.userManager.AddToRoleAsync(user, GlobalConstants.AdminRole);
            }
            else
            {
                addRoleResult = await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRole);
            }

            if (!addRoleResult.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<IdentityResult> UpdateUser(DetailsUserViewModel model)
        {
            User user = await this.GetUserById(model.Id);

            user.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.PersonalNumber = model.PersonalNumber;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.IsAdministrator = model.IsAdministrator;

            return await this.userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> RemoveUser(string id)
        {
            var user = this.userManager.FindByIdAsync(id).Result;
            return await this.userManager.DeleteAsync(user);
        }

        public async Task<bool> Login(LoginUserViewModel model)
        {
            var user = this.GetUser(model.Username).Result;
            if (user == null)
            {
                return false;
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async void Logout()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<User> GetUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            return user;
        }
    }
}
