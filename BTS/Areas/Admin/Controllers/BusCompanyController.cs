using BTS.Models;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BTS.Areas.Admin.Controllers
{
    public class BusCompanyController : Controller
    {

        private IUnitOfWork _unitOfWork;
        public BusCompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var tableCount = _unitOfWork.BusCompany.GetAll().Count();
            var obj = _unitOfWork.BusCompany.GetAll().
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(tableCount / (double)pageSize);

            if (obj == null)
                return NotFound();
            return View(obj);
        }
        public IActionResult Upsert(string? BusCompanyId)
        {
            if (BusCompanyId == null)
            {
                return View();
            }
            else
            {
                var busCompany = _unitOfWork.BusCompany.Get(x => x.BusCompanyId == BusCompanyId);
                return View(busCompany);
            }

        }
        [HttpPost]
        public IActionResult Upsert(BusCompanies busCompany)
        {

            var nameExists = _unitOfWork.BusCompany.Get(x => x.CompanyName == busCompany.CompanyName && x.BusCompanyId != busCompany.BusCompanyId);
            var emailExists = _unitOfWork.BusCompany.Get(x => x.Email == busCompany.Email && x.BusCompanyId != busCompany.BusCompanyId);

            var errors = new List<string>();

            if (nameExists != null) errors.Add("Company name");
            if (emailExists != null) errors.Add("Email");

            if (errors.Any())
            {
                TempData["error"] = $"{string.Join(" and ", errors)} already exists.";
                return View(busCompany);
            }

            if (ModelState.IsValid)
            {
                string message = "";
                if (string.IsNullOrEmpty(busCompany.BusCompanyId))
                {
                    busCompany.BusCompanyId = $"BUS-COMPANY-{Guid.NewGuid().ToString().Substring(0, 5)}";
                    _unitOfWork.BusCompany.Add(busCompany);
                    message = "added";
                }
                else
                {
                    _unitOfWork.BusCompany.Update(busCompany);
                    message = "updated";
                }

                TempData["success"] = $"Bus company {message} successfully";
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
           
            return View(busCompany);
                
        }

        //[HttpDelete]- 
        public IActionResult Delete(string BusCompanyId)
        {
            var objDelete = _unitOfWork.BusCompany.Get(x => x.BusCompanyId == BusCompanyId);
            if (objDelete == null)
            {
                TempData["error"] = "Failed to delete, please try again!";
                return RedirectToAction("Index");
            }

            _unitOfWork.BusCompany.Remove(objDelete);
            _unitOfWork.Save();
            TempData["success"] = "Bus companay deleted successfully";

            return RedirectToAction("Index");
        }

    }
}
