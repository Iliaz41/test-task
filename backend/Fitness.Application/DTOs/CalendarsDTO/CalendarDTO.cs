namespace Fitness.Application.DTOs.CalendarsDTO
{
    public class CalendarDTO
    {
        public long Id { get; set; }
        public DateTime Day { get; set; }
        public bool IsToday => Day.Date == DateTime.UtcNow.Date;
    }
}
