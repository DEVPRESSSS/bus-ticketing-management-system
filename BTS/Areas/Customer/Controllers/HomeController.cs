using BTS.Models;
using BTS.Models.ViewModel;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BTS.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var listOfAvailableBus = _unitOfWork.Schedule.GetAll(includeProperties: "Route.OriginStation,Route.DestinationStation,Buses,Buses.BusCompany,Buses.BusType").ToList();
            if(listOfAvailableBus == null)
            {
                TempData["Error"] = "No bus schedule available!!!";
                return View();
            }

            var obj = new BookingVM
            {
                Schedules = listOfAvailableBus,
            };

            ViewBag.Routes = _unitOfWork.BusRoute.GetAll(includeProperties: "OriginStation,DestinationStation").Select(u => new SelectListItem
            {
                Text = $"{u.OriginStation?.StationName + " To " + u.DestinationStation?.StationName}",
                Value = u.RouteId
            });
            return View(obj);
        }

        [HttpGet]
        public IActionResult Search(BookingVM bookingVM)
        {
            var searchDate = bookingVM.SelectedDate.Date;

            var availableSchedOfBuses = _unitOfWork.Schedule
                .GetAll(x => x.DepartureTime.Date == searchDate || x.RouteId == bookingVM.From && x.RouteId == bookingVM.To,
                 includeProperties: "Route.OriginStation,Route.DestinationStation,Buses,Buses.BusCompany,Buses.BusType")
                .ToList();

            var obj = new BookingVM
            {
                SelectedDate = searchDate,
                From = bookingVM.From,
                To = bookingVM.To,
                Schedules = availableSchedOfBuses
            };

           
            ViewBag.Routes = _unitOfWork.BusRoute
                .GetAll(includeProperties: "OriginStation,DestinationStation")
                .Select(u => new SelectListItem
                {
                    Text = $"{u.OriginStation?.StationName} To {u.DestinationStation?.StationName}",
                    Value = u.RouteId
                });

            return View("Index", obj); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmBooking(BookingVM obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ScheduleId))
            {
                TempData["Error"] = "Failed to avail ticket.";
                return RedirectToAction("Index");
            }

            var schedule = _unitOfWork.Schedule.Get(
                x => x.ScheduleId == obj.ScheduleId,
                includeProperties: "Route,Buses"
            );

            if (schedule == null)
            {
                TempData["Error"] = "Schedule not found.";
                return RedirectToAction("Index");
            }
       
            var availableSeats = _unitOfWork.Seat
                .GetAll(x => x.BusId == schedule.BusId && x.isActive == true)
                .Take(obj.SeatCount)
                .ToList();

            if (!availableSeats.Any())
            {
                TempData["Error"] = "No available seats.";
                return RedirectToAction("Index");
            }


            var userId = _userManager.GetUserId(User);

            foreach (var seat in availableSeats)
            {
                var ticket = new Tickets
                {
                    TicketId = $"TICKET-{Guid.NewGuid().ToString()[..5]}",
                    TicketCode = $"TCKT-{Guid.NewGuid().ToString()[..5]}",
                    UserId = userId,
                    SeatId = seat.SeatId,
                    ScheduledId = obj.ScheduleId,
                    AmountPaid = schedule.Route?.BasePrice ?? 0,
                    Status = "Confirmed",
                    BookedAt = DateTime.Now,
                    CancelledAt = null
                };

                seat.isActive = false;

                _unitOfWork.Ticket.Add(ticket);
                _unitOfWork.Seat.Update(seat);
            }

            schedule.AvailableSeats -= obj.SeatCount;
            _unitOfWork.Schedule.Update(schedule);
            _unitOfWork.Save();

            TempData["Success"] = $"{obj.SeatCount} ticket(s) booked successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult MyBooking()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var tickets = _unitOfWork.Ticket
                .GetAll(
                    x => x.UserId == userId,
                    includeProperties: "Schedules.Route.OriginStation,Schedules.Route.DestinationStation,Schedules.Buses.BusType,Seats"
                )
                .OrderByDescending(x => x.BookedAt)
                .ToList();

            return View(tickets);
        }
    }
}
