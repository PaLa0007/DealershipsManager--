using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealershipsManager.Data.Entities;
using DealershipsManager.Models.Filters;
using DealershipsManager.Models.Shared;
using DealershipsManager.Models.User;
using DealershipsManager.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DealershipsManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewAll(UserIndexViewModel model)
        {
            model.Pager ??= model.Pager ?? new PagerViewModel();
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter ??= model.Filter ?? new FilterUserViewModel();

            bool emptyUsername = string.IsNullOrWhiteSpace(model.Filter.Username);
            bool emptyFirstName = string.IsNullOrWhiteSpace(model.Filter.FirstName);
            bool emptySurname = string.IsNullOrWhiteSpace(model.Filter.LastName);
            bool emptyEmail = string.IsNullOrWhiteSpace(model.Filter.Email);

            List<User> query = await this.userService.GetAllUsersAsync(u =>
                 (emptyUsername || u.UserName.Contains(model.Filter.Username)) &&
                 (emptyFirstName || u.FirstName.Contains(model.Filter.FirstName)) &&
                 (emptySurname || u.LastName.Contains(model.Filter.LastName)) &&
                 (emptyEmail || u.Email.Contains(model.Filter.Email)));

            model.Pager.PagesCount = (int)Math.Ceiling(query.Count() / (double)model.Pager.ItemsPerPage);

            query = query.OrderBy(u => u.Id).Skip((model.Pager.Page - 1) * model.Pager.ItemsPerPage).Take(model.Pager.ItemsPerPage).ToList();

            model.Items = query.Select(u => new DetailsUserViewModel
            {
                Id = u.Id,
                Username = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
        {
            bool result = userService.AddUser(model).Result;
            if (!result)
            {
                return this.View(model);
            }
            else
            {
                return Redirect("/Home");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel model)
        {
            bool result = userService.Login(model).Result;
            if (!result)
            {
                return this.View(model);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await this.userService.GetUserById(id);

            DetailsUserViewModel model = new DetailsUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Address = user.LastName,
                PersonalNumber = user.PersonalNumber,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsAdministrator = user.IsAdministrator
            };

            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            await this.userService.RemoveUser(id);
            return Redirect("/User");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await this.userService.GetUserById(id);

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Address = user.LastName,
                PersonalNumber = user.PersonalNumber,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsAdministrator = user.IsAdministrator
            };

            return View(model);

        }

        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> Edit(DetailsUserViewModel model)
        {
            await this.userService.UpdateUser(model);
            return Redirect("/Home");
        }

        public IActionResult Logout()
        {
            userService.Logout();
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            User user = await this.userService.GetUserById(id);

            DetailsUserViewModel model = new DetailsUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PersonalNumber = user.PersonalNumber,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsAdministrator = user.IsAdministrator
            };

            return View(model);
        }
    }
}