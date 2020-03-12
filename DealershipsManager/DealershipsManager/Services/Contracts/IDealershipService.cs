using DealershipsManager.Data.Entities;
using DealershipsManager.Models.Dealership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DealershipsManager.Services.Contracts
{
    interface IDealershipService
    {

        IQueryable<Dealership> GetAllDealerships();

        IQueryable<Dealership> GetAllDealerships(Expression<Func<Dealership, bool>> predicate);

        Dealership GetOneDealership(int id);

        Dealership GetOneDealership(Expression<Func<Dealership, bool>> predicate);

        void AddDealership(DealershipInputModel entity);

        void UpdateDealership(DealershipEditViewModel entity);

        void RemoveDealership(DealershipDetailsViewModel entity);

    }
}
