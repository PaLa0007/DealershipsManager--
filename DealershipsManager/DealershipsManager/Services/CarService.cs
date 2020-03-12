using DealershipsManager.Data;
using DealershipsManager.Data.Entities;
using DealershipsManager.Models.Car;
using DealershipsManager.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DealershipsManager.Services
{
    public class CarService : ICarService
    {
        public readonly DealershipsManagerDbContext context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public CarService(DealershipsManagerDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void AddCar(CarInputModel entity)
        {
            Car car = new Car
            {
                Manufacturer = entity.Manufacturer,
                Model = entity.Model,
                Type = entity.Type,
                Engine = entity.Engine,
                Horsepower = entity.Horsepower,
                Transmission = entity.Transmission,
                Gears = entity.Gears,
                Color = entity.Color,
                UserId = entity.UserId,
                User = entity.User,
                Dealership = entity.Dealership,
                DealershipId = entity.DealershipId
            };

            context.Cars.Add(car);
            context.SaveChanges();
        }

        public IQueryable<Car> GetAllCars()
        {
            return this.context.Cars.AsQueryable();
        }

        public IQueryable<Car> GetAllCars(Expression<Func<Car, bool>> predicate)
        {
            return this.context.Cars.Where(predicate).AsQueryable();
        }

        public Car GetOneCar(int id)
        {
            return context.Cars.Find(id);
        }

        public Car GetOneCar(Expression<Func<Car, bool>> predicate)
        {
            return this.context.Cars.FirstOrDefault(predicate);
        }

        public void RemoveCar(CarEditInputModel entity)
        {
            Car car = context.Cars.Find(entity.CarId);
            context.Cars.Remove(car);
            context.SaveChanges();
        }

        public void UpdateCar(CarEditInputModel entity)
        {
            Car car = context.Cars.Find(entity.CarId);
            car.Manufacturer = entity.Manufacturer;
            car.Model = entity.Model;
            car.Type = entity.Type;
            car.Engine = entity.Engine;
            car.Horsepower = entity.Horsepower;
            car.Transmission = entity.Transmission;
            car.Gears = entity.Gears;
            car.Color = entity.Color;
            car.UserId = entity.UserId;
            car.User = entity.User;

            context.Cars.Update(car);
            context.SaveChanges();
        }

        public Car GetById(int id)
        {
            return context.Cars.Find(id);
        }

        public CarInputModel PriparedPage(string userName)
        {

            var model = new CarInputModel();
            model.AllDealerships = GetAllDealerships();


            Task.Run(async () =>
            {
                User loggedUser = await userManager.FindByNameAsync(userName);
                model.UserId = loggedUser.Id;
                model.User = loggedUser;

            }).Wait();


            return model;
        }

        private List<Dealership> GetAllDealerships()
        {
            return context.Dealerships.ToList();

        }
    }
}
