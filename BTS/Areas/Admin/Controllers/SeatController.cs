using BTS.Areas.Service.SD;
using BTS.Models;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTS.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SeatController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public SeatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var tableCount = _unitOfWork.Seat.GetAll(includeProperties: "Buses").Count();
            var obj = _unitOfWork.Seat.GetAll().
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(tableCount / (double)pageSize);

            if (obj == null)
                return NotFound();
            return View(obj);
        }
        public IActionResult Upsert(string? seatId)
        {
            ViewBag.Buses = _unitOfWork.Bus.GetAll().Select(u => new SelectListItem
            {
                Text = u.BusName,
                Value = u.BusId
            });

            if (seatId == null)
            {
                return View();
            }
            else
            {
                var busType = _unitOfWork.Seat.Get(x => x.SeatId == seatId);
                return View(busType);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Seats seat)
        {

            var nameExists = _unitOfWork.Seat.Get(x => x.SeatNumber == seat.SeatNumber && x.SeatId != seat.SeatId);
            if (nameExists != null)
            {
                TempData["error"] = "Seat number is already exist for this bus!";

                ViewBag.Buses = _unitOfWork.Bus.GetAll().Select(u => new SelectListItem
                {
                    Text = u.BusName,
                    Value = u.BusId
                });

                return View(seat);
            }

            if (ModelState.IsValid)
            {
                string message = "";
                if (string.IsNullOrEmpty(seat.SeatId))
                {
                    seat.SeatId = $"SEAT-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";
                    _unitOfWork.Seat.Add(seat);
                    message = "added";
                }
                else
                {
                    _unitOfWork.Seat.Update(seat);
                    message = "updated";
                }

                _unitOfWork.Save();
                TempData["success"] = $"Seat {message} successfully";

                return RedirectToAction("Index");
            }

            ViewBag.Buses = _unitOfWork.Bus.GetAll().Select(u => new SelectListItem
            {
                Text = u.BusName,
                Value = u.BusId
            });

            return View(seat);

        }

        //[HttpDelete]- 
        public IActionResult Delete(string seatId)
        {
            var objDelete = _unitOfWork.Seat.Get(x => x.SeatId == seatId);
            if (objDelete == null)
            {
                TempData["error"] = "Failed to delete, please try again!";
                return RedirectToAction("Index");
            }

            _unitOfWork.Seat.Remove(objDelete);
            _unitOfWork.Save();
            TempData["success"] = "Seat deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
