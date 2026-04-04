using BTS.Models;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BusRouteController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public BusRouteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var tableCount = _unitOfWork.BusRoute.GetAll(includeProperties: "OriginStation,DestinationStation").Count();
            var obj = _unitOfWork.BusRoute.GetAll().
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(tableCount / (double)pageSize);

            if (obj == null)
                return NotFound();
            return View(obj);
        }
        public IActionResult Upsert(string? routeId)
        {
            ViewBag.OriginStationAndDestinationStation = _unitOfWork.Station.GetAll().Select(u => new SelectListItem
            {
                Text = u.StationName,
                Value = u.StationId
            });

            if (routeId == null)
            {
                return View();
            }
            else
            {
                var busType = _unitOfWork.BusRoute.Get(x => x.RouteId == routeId);
                return View(busType);
            }

        }
        [HttpPost]
        public IActionResult Upsert(BusRoutes busRoutes)
        {

            //var destination = _unitOfWork.BusRoute.Get(x => x.DestinationId == busRoutes.StationId);
            if (busRoutes.DestinationId == busRoutes.StationId)
            {
                TempData["error"] = "Invalid destination, please select unique origin station or destination";

                return View(busRoutes);
            }

            if (ModelState.IsValid)
            {
                string message = "";
                if (string.IsNullOrEmpty(busRoutes.RouteId))
                {
                    busRoutes.RouteId = $"ROUTE-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";
                    _unitOfWork.BusRoute.Add(busRoutes);
                    message = "added";
                }
                else
                {
                    _unitOfWork.BusRoute.Update(busRoutes);
                    message = "updated";
                }

                _unitOfWork.Save();
                TempData["success"] = $"Bus route {message} successfully";

                return RedirectToAction("Index");
            }

            ViewBag.OriginStationAndDestinationStation = _unitOfWork.Station.GetAll().Select(u => new SelectListItem
            {
                Text = u.StationName,
                Value = u.StationId
            });

            return View(busRoutes);

        }

        //[HttpDelete]- 
        public IActionResult Delete(string routeId)
        {
            var objDelete = _unitOfWork.BusRoute.Get(x => x.RouteId == routeId);
            if (objDelete == null)
            {
                TempData["error"] = "Failed to delete, please try again!";
                return RedirectToAction("Index");
            }

            _unitOfWork.BusRoute.Remove(objDelete);
            _unitOfWork.Save();
            TempData["success"] = "Bus route deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
