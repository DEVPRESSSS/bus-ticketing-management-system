using BTS.Models;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ScheduleController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ScheduleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var tableCount = _unitOfWork.Schedule.GetAll().Count();
            var obj = _unitOfWork.Schedule
            .GetAll(includeProperties: "Route.OriginStation,Route.DestinationStation,Buses")
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(tableCount / (double)pageSize);

            if (obj == null)
                return NotFound();
            return View(obj);
        }
        public IActionResult Upsert(string? scheduleId)
        {
            ViewBag.Routes = _unitOfWork.BusRoute.GetAll(includeProperties: "OriginStation,DestinationStation").Select(u => new SelectListItem
            {
                Text = $"{u.OriginStation?.StationName + " To " + u.DestinationStation?.StationName}",
                Value = u.RouteId
            });

            ViewBag.Buses = _unitOfWork.Bus.GetAll().Select(u => new SelectListItem
            {
                Text = u.BusName,
                Value = u.BusId
            });
            if (scheduleId == null)
            {
                return View();
            }
            else
            {
                var bus = _unitOfWork.Schedule.Get(x => x.ScheduleId == scheduleId);
                return View(bus);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Schedules schedules)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(schedules.ScheduleId))
                {
                    schedules.ScheduleId = $"SCHED-{Guid.NewGuid().ToString().Substring(0, 5)}";
                    _unitOfWork.Schedule.Add(schedules);
                    TempData["success"] = "Schedule registered successfully";
                }
                else
                    _unitOfWork.Schedule.Update(schedules);

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(schedules);
        }

        //[HttpDelete]- 
        public IActionResult Delete(string scheduleId)
        {
            var objDelete = _unitOfWork.Schedule.Get(x => x.ScheduleId == scheduleId);
            if (objDelete == null)
            {
                TempData["error"] = "Failed to delete, please try again!";
                return RedirectToAction("Index");
            }

            _unitOfWork.Schedule.Remove(objDelete);
            _unitOfWork.Save();
            TempData["success"] = "Schedule deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
