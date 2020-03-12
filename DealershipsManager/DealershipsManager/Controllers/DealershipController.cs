using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealershipsManager.Data.Entities;
using DealershipsManager.Initialisation;
using DealershipsManager.Models.Dealership;
using DealershipsManager.Models.Filters;
using DealershipsManager.Models.Shared;
using DealershipsManager.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealershipsManager.Controllers
{
    public class DealershipController : Controller
    {
        private readonly IDealershipService service;

        public DealershipController(IDealershipService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DealershipInputModel model)
        {
            service.AddDealership(model);
            return Redirect("/");
        }



        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult Edit(int id)
        {
            Dealership dealership = service.GetOneDealership(id);
            DealershipEditViewModel model = new DealershipEditViewModel
            {
                DealershipId = dealership.DealershipId,
                Name = dealership.Name,
                Country = dealership.Country,
                Town = dealership.Town,
                Cars = dealership.Cars
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult Edit(DealershipEditViewModel entity)
        {
            service.UpdateDealership(entity);

            return Redirect("/");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Dealership dealership = service.GetOneDealership(id);
            DealershipDetailsViewModel model = new DealershipDetailsViewModel
            {
                DealershipId = dealership.DealershipId,
                Name = dealership.Name,
                Country = dealership.Country,
                Town = dealership.Town,
                Cars = dealership.Cars
            };

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult Delete(int id)
        {
            Dealership dealership = service.GetOneDealership(id);
            DealershipDetailsViewModel model = new DealershipDetailsViewModel
            {
                DealershipId = dealership.DealershipId,
                Name = dealership.Name,
                Country = dealership.Country,
                Town = dealership.Town,
                Cars = dealership.Cars
            };

            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult Delete(DealershipDetailsViewModel entity)
        {
            service.RemoveDealership(entity);
            return Redirect("/");
        }


        [HttpGet]
        public async Task<IActionResult> Index(DealershipIndexViewModel model)
        {

            model.Pager ??= new PagerViewModel();
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter ??= model.Filter ?? new FilterDealershipViewModel();

            bool emptyName = string.IsNullOrWhiteSpace(model.Filter.Name);
            bool emptyCountry = string.IsNullOrWhiteSpace(model.Filter.Country);
            bool emptyTown = string.IsNullOrWhiteSpace(model.Filter.Town);


            IQueryable<Dealership> dealerships = this.service.GetAllDealerships(u =>
                  (emptyName || u.Name.Contains(model.Filter.Name)) &&
                  (emptyCountry || u.Country.Contains(model.Filter.Country)) &&
                  (emptyTown || u.Town.Contains(model.Filter.Town)));

            model.Pager.PagesCount = (int)Math.Ceiling(dealerships.Count() / (double)model.Pager.ItemsPerPage);

            dealerships = dealerships.OrderBy(u => u.DealershipId).Skip((model.Pager.Page - 1) * model.Pager.ItemsPerPage).Take(model.Pager.ItemsPerPage);

            model.Items = dealerships.Select(u => new DealershipDetailsViewModel
            {
                DealershipId = u.DealershipId,
                Name = u.Country,
                Country = u.Country,
                Town = u.Town,
                Cars = u.Cars,

            }).ToList();

            return View(model);
        }
    }
}