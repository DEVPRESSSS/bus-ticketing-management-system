using BTS.Areas.Service.SD;
using BTS.Models;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Areas.Admin.Controllers
{
    [Authorize]

    [Area("Admin")]
    public class BusTypeController : Controller
    {
   
        private IUnitOfWork _unitOfWork;
        public BusTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var tableCount = _unitOfWork.BusType.GetAll().Count();
            var obj = _unitOfWork.BusType.GetAll().
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(tableCount / (double)pageSize);

            if (obj == null)
                return NotFound();
            return View(obj);
        }
        public IActionResult Upsert(string? busTypeId)
        {
            if (busTypeId == null)
            {
                return View();
            }
            else
            {
                var busType = _unitOfWork.BusType.Get(x => x.BusTypeId == busTypeId);
                return View(busType);
            }

        }
        [HttpPost]
        public IActionResult Upsert(BusType busType)
        {

            var nameExists = _unitOfWork.BusType.Get(x => x.BusTypeName == busType.BusTypeName && x.BusTypeId != busType.BusTypeId);
            if (nameExists != null)
            {
                TempData["error"] =  "Bus type name is already exist!";
                return View(busType);
            }

            if (ModelState.IsValid)
            {
                string message = "";
                if (string.IsNullOrEmpty(busType.BusTypeId))
                {
                    busType.BusTypeId = $"BUS-TYPE-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";
                    _unitOfWork.BusType.Add(busType);
                    message = "added";
                }
                else
                {
                    _unitOfWork.BusType.Update(busType);
                    message = "updated";
                }

                _unitOfWork.Save();
                TempData["success"] = $"Bus type {message} successfully";

                return RedirectToAction("Index");
             }

            return View(busType);

        }

        //[HttpDelete]- 
        public IActionResult Delete(string busTypeId)
        {
            var objDelete = _unitOfWork.BusType.Get(x => x.BusTypeId == busTypeId);
            if (objDelete == null)
            {
                TempData["error"] = "Failed to delete, please try again!";
                return RedirectToAction("Index");
            }

            _unitOfWork.BusType.Remove(objDelete);
            _unitOfWork.Save();
            TempData["success"] = "Bus type deleted successfully";

            return RedirectToAction("Index");
        }
     
    }
}
