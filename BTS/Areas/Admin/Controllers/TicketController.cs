using BTS.Areas.Service.SD;
using BTS.Models;
using BTS.Models.ViewModel;
using BTS.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTS.Areas.Admin.Controllers
{
    [Area(SdRoles.Admin)]
    public class TicketController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var listOfBookings = _unitOfWork.Ticket.GetAll(
                includeProperties: "ApplicationUser,Seats,Seats.Buses,Schedules,Schedules.Buses,Schedules.Route,Schedules.Route.OriginStation,Schedules.Route.DestinationStation"
            ).ToList();

            if (listOfBookings == null || !listOfBookings.Any())
            {
                return View(new List<Tickets>());
            }

            return View(listOfBookings);
        }
        public IActionResult Approve(string ticketId)
        {
            var ticket = _unitOfWork.Ticket.Get(t => t.TicketId == ticketId);
            if (ticket == null) return NotFound();

            ticket.Status = "Approved";
            _unitOfWork.Ticket.Update(ticket);
            _unitOfWork.Save();

            TempData["Success"] = "Ticket approved successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult Reject(string ticketId)
        {
            var ticket = _unitOfWork.Ticket.Get(t => t.TicketId == ticketId);
            if (ticket == null) return NotFound();

            ticket.Status = "Rejected";
            ticket.CancelledAt = DateTime.Now;
            _unitOfWork.Ticket.Update(ticket);
            _unitOfWork.Save();

            TempData["Error"] = "Ticket rejected.";
            return RedirectToAction("Index");
        }

    }
}
