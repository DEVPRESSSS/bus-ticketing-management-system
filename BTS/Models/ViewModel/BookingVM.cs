using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BTS.Models.ViewModel
{
    public class BookingVM
    {
        public DateTime SelectedDate { get; set; }
        public string?  From { get; set; }
        public string?  To { get; set; }
        public string?  SeatId { get; set; }
        public int  SeatCount { get; set; }
        public string? ScheduleId { get; set; }

        [ValidateNever]
        public List<Schedules>? Schedules { get; set; }
    }
}
