using BTS.Areas.Service.SD;
using BTS.Models;
using BTS.Repositories;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BTS.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BusController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public BusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var tableCount = _unitOfWork.Bus.GetAll().Count();
            var obj = _unitOfWork.Bus.GetAll(includeProperties: "BusType,BusCompany").
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int) Math.Ceiling(tableCount / (double)pageSize);

            if (obj == null)
                return NotFound();
            return View(obj);
        }
        public IActionResult Upsert(string? BusId)
        {
            var busCompanies = _unitOfWork.BusCompany.GetAll().Select(u => new SelectListItem
                {
                    Text= u.CompanyName,
                    Value= u.BusCompanyId
                });
            ViewBag.BusCompanies = busCompanies;

            var busType = _unitOfWork.BusType.GetAll().Select(u => new SelectListItem
            {
                Text = u.BusTypeName,
                Value = u.BusTypeId
            });
            ViewBag.BusTypes = busType;

            if (BusId == null)
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

            var plateNumberExists = _unitOfWork.Bus.Get(x => x.PlateNumber == buses.PlateNumber && x.BusId != buses.BusId);
            var busNumberExists = _unitOfWork.Bus.Get(x => x.BusNumber == buses.BusNumber && x.BusId != buses.BusId);

            var errors = new List<string>();

            if (plateNumberExists != null) errors.Add("Plate number");
            if (busNumberExists != null) errors.Add("Bus number");

            if (errors.Any())
            {
                TempData["error"] = $"{string.Join(" and ", errors)} already exists.";

                ViewBag.BusCompanies = _unitOfWork.BusCompany.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.CompanyName,
                    Value = u.BusCompanyId
                });

                ViewBag.BusTypes = _unitOfWork.BusType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.BusTypeName,
                    Value = u.BusTypeId
                });

                return View(buses);
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(buses.BusId))
                {
                    buses.BusId = $"BUS-{Guid.NewGuid().ToString().Substring(0, 5)}";
                    _unitOfWork.Bus.Add(buses);
                    TempData["success"] = "Bus registered successfully";
                }
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

            ViewBag.BusTypes = _unitOfWork.BusType.GetAll().Select(u => new SelectListItem
            {
                Text = u.BusTypeName,
                Value = u.BusTypeId
            });

         

            return View(buses);
        }

        //[HttpDelete]- 
        public IActionResult Delete(string BusId)
        {
            var objDelete = _unitOfWork.Bus.Get(x=>x.BusId == BusId);
            if (objDelete == null)
            {
                TempData["error"] = "Failed to delete, please try again!";
                return RedirectToAction("Index");
            }
               
            _unitOfWork.Bus.Remove(objDelete);
            _unitOfWork.Save();
            TempData["success"] = "Bus deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
