using DealershipsManager.Data.Entities;
using DealershipsManager.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DealershipsManager.Services.Contracts
{
    public interface ICarService
    {
        CarInputModel PriparedPage(string userName);

        IQueryable<Car> GetAllCars();

        IQueryable<Car> GetAllCars(Expression<Func<Car, bool>> predicate);

        Car GetOneCar(int id);

        Car GetOneCar(Expression<Func<Car, bool>> predicate);

        void AddCar(CarInputModel entity);

        void UpdateCar(CarEditInputModel entity);

        void RemoveCar(CarEditInputModel entity);
    }
}
