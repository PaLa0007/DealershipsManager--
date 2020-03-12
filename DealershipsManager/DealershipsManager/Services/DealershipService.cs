using DealershipsManager.Data;
using DealershipsManager.Data.Entities;
using DealershipsManager.Models.Dealership;
using DealershipsManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DealershipsManager.Services
{
    public class DealershipService : IDealershipService
    {
        public readonly DealershipsManagerDbContext context;

        public DealershipService(DealershipsManagerDbContext context)
        {
            this.context = context;
        }

        public void AddDealership(DealershipInputModel model)
        {
            Dealership dealership = new Dealership
            {
                Name = model.Name,
                Country = model.Country,
                Town = model.Town,
                Cars = model.Cars,
            };

            context.Dealerships.Add(dealership);
            context.SaveChanges();
        }

        public IQueryable<Dealership> GetAllDealerships()
        {
            return this.context.Dealerships.AsQueryable();
        }

        public IQueryable<Dealership> GetAllDealerships(Expression<Func<Dealership, bool>> predicate)
        {
            return this.context.Dealerships.Where(predicate).AsQueryable();
        }

        public Dealership GetOneDealership(int id)
        {
            return context.Dealerships.FirstOrDefault(f => f.DealershipId == id);
        }

        public Dealership GetOneDealership(Expression<Func<Dealership, bool>> predicate)
        {
            return this.context.Dealerships.FirstOrDefault(predicate);
        }

        public void RemoveDealership(DealershipDetailsViewModel entity)
        {
            Dealership dealership = context.Dealerships.Find(entity.DealershipId);
            context.Dealerships.Remove(dealership);
            context.SaveChanges();
        }

        public void UpdateDealership(DealershipEditViewModel entity)
        {
            Dealership dealership = context.Dealerships.Find(entity.DealershipId);
            if (dealership is null)
            {
                return;
            }
            dealership.Name = entity.Name;
            dealership.Country = entity.Country;
            dealership.Town = entity.Town;
            dealership.Cars = entity.Cars;

            context.Dealerships.Update(dealership);
            context.SaveChanges();
        }
    }
}
