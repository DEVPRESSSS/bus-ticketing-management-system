using BTS.Models;
using BTS.Repositories;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTS.Areas.Admin.Controllers
{
    public class BusController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public BusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //var obj = _unitOfWork.Bus.GetAll().ToList();
            //if(obj == null)
            //    return NotFound();
            return View();
        }
        public IActionResult Upsert(string? BusId)
        {
            var busCompanies = _unitOfWork.BusCompany.GetAll().Select(u => new SelectListItem
                {
                    Text= u.CompanyName,
                    Value= u.BusCompanyId
                });
            ViewBag.BusCompanies = busCompanies;
             
            if(BusId == null)
            {
                return View();
            }
            else
            {
                var bus = _unitOfWork.Bus.Get(x=>x.BusId == BusId, includeProperties: "BusCompany");
                return View(bus);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Buses buses)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(buses.BusId))
                    _unitOfWork.Bus.Add(buses);
                else
                    _unitOfWork.Bus.Update(buses);

                _unitOfWork.Save();
                return RedirectToAction("Index"); 
            }

            ViewBag.BusCompanies = _unitOfWork.BusCompany.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.CompanyName,
                    Value = u.BusCompanyId
                });

            return View(buses);
        }
    }
}
