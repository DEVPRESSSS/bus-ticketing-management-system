using BTS.Models;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTS.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class StationController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public StationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var tableCount = _unitOfWork.Station.GetAll().Count();
            var obj = _unitOfWork.Station.GetAll().
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(tableCount / (double)pageSize);

            if (obj == null)
                return NotFound();
            return View(obj);
        }
        public IActionResult Upsert(string? StationId)
        {

            if (StationId == null)
            {
                return View();
            }
            else
            {
                var bus = _unitOfWork.Station.Get(x => x.StationId == StationId);
                return View(bus);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Stations station)
        {

            var stationNameExist = _unitOfWork.Station.Get(x => x.StationName == station.StationName && x.StationId != station.StationId);
            var addressExist = _unitOfWork.Station.Get(x => x.Addresss == station.Addresss && x.StationId != station.StationId);

            var errors = new List<string>();

            if (stationNameExist != null) errors.Add("Station name");
            if (addressExist != null) errors.Add("Address");

            if (errors.Any())
            {
                TempData["error"] = $"{string.Join(" and ", errors)} already exists.";
                return View(station);
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(station.StationId))
                {
                    station.StationId = $"STATION-{Guid.NewGuid().ToString().Substring(0, 5)}";
                    _unitOfWork.Station.Add(station);
                    TempData["success"] = "Station registered successfully";
                }
                else
                    _unitOfWork.Station.Update(station);

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(station);
        }

        //[HttpDelete]- 
        public IActionResult Delete(string stationId)
        {
            var objDelete = _unitOfWork.Station.Get(x => x.StationId == stationId);
            if (objDelete == null)
            {
                TempData["error"] = "Failed to delete, please try again!";
                return RedirectToAction("Index");
            }

            _unitOfWork.Station.Remove(objDelete);
            _unitOfWork.Save();
            TempData["success"] = "Station deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
