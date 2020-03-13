using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealershipsManager.Data.Entities;
using DealershipsManager.Models.Car;
using DealershipsManager.Models.Filters;
using DealershipsManager.Models.Shared;
using DealershipsManager.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DealershipsManager.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService service;
        private readonly UserManager<User> userManager;

        public CarController(ICarService service, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var user = userManager.GetUserAsync(User).Result;

            CarInputModel model = service.PriparedPage(user.UserName);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CarInputModel model)
        {
            service.AddCar(model);
            return Redirect("/");
        }




        public IActionResult Edit(int id)
        {
            Car car = service.GetOneCar(id);
            CarEditInputModel model = new CarEditInputModel
            {
                CarId = car.CarId,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                Type = car.Type,
                Engine = car.Engine,
                Horsepower = car.Horsepower,
                Transmission = car.Transmission,
                Gears = car.Gears,
                Color = car.Color,
                Price = car.Price,
                IsSold = car.IsSold,
                UserId = car.UserId,
                User = car.User,
                DealershipId = car.DealershipId,
                Dealership = car.Dealership
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CarEditInputModel entity)
        {
            service.UpdateCar(entity);

            return Redirect("/");
        }


        [HttpGet]
        public async Task<IActionResult> Index(CarIndexViewModel model)
        {
            model.Pager ??= model.Pager ?? new PagerViewModel();
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter ??= model.Filter ?? new FilterCarViewModel();

            bool emptyManufacturer = string.IsNullOrWhiteSpace(model.Filter.Manufacturer);
            bool emptyModel = string.IsNullOrWhiteSpace(model.Filter.Model);
            bool emptyColor = string.IsNullOrEmpty(model.Filter.Color);


            IQueryable<Car> query = this.service.GetAllCars(u =>
                 (emptyManufacturer || u.Manufacturer.Contains(model.Filter.Manufacturer)) &&
                 (emptyModel || u.Model.Contains(model.Filter.Model)) &&
                 (emptyColor || u.Color.Contains(model.Filter.Color)));

            model.Pager.PagesCount = (int)Math.Ceiling(query.Count() / (double)model.Pager.ItemsPerPage);

            query = query.OrderBy(u => u.DealershipId).Skip((model.Pager.Page - 1) * model.Pager.ItemsPerPage).Take(model.Pager.ItemsPerPage);

            model.Items = query.Select(u => new CarViewModel
            {
                CarId = u.CarId,
                DealershipId = u.DealershipId,
                Manufacturer = u.Manufacturer,
                Model = u.Model,
                Type = u.Type,
                Engine = u.Engine,
                Horsepower = u.Horsepower,
                Transmission = u.Transmission,
                Gears = u.Gears,
                Color = u.Color,
                UserId = u.UserId,
                User = u.User,
                Dealership = u.Dealership

            }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Car car = service.GetOneCar(id);
            CarViewModel model = new CarViewModel
            {
                CarId = car.CarId,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                Type = car.Type,
                Engine = car.Engine,
                Horsepower = car.Horsepower,
                Transmission = car.Transmission,
                Gears = car.Gears,
                Color = car.Color,
                Price = car.Price,
                IsSold = car.IsSold,
                UserId = car.UserId,
                User = car.User,
                DealershipId = car.DealershipId,
                Dealership = car.Dealership
            };

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Car car = service.GetOneCar(id);
            CarEditInputModel model = new CarEditInputModel
            {
                CarId = car.CarId,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                Type = car.Type,
                Engine = car.Engine,
                Horsepower = car.Horsepower,
                Transmission = car.Transmission,
                Gears = car.Gears,
                Color = car.Color,
                Price = car.Price,
                IsSold = car.IsSold,
                UserId = car.UserId,
                User = car.User,
                DealershipId = car.DealershipId,
                Dealership = car.Dealership
            };

            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(CarEditInputModel entity)
        {
            service.RemoveCar(entity);
            return Redirect("/");
        }
    }
}