using Fitness.Application.DTOs.CalendarsDTO;

namespace Fitness.Application.IServices
{
    public interface ICalendarService
    {
        Task<CalendarDTO> CreateCalendarAsync(CreateCalendarDTO createCalendarDTO, CancellationToken tokenCancel);
        Task<List<CalendarDTO?>> GetAllCalendarsAsync(CancellationToken tokenCancel);
        Task<CalendarDTO> UpdateCalendarAsync(long id, UpdateCalendarDTO calendarDTO, CancellationToken tokenCancel);
        Task DeleteCalendarAsync(long id, CancellationToken tokenCancel);
        Task<CalendarDTO?> GetCalendarByIdAsync(long id, CancellationToken tokenCancel);
        Task<CalendarDTO?> GetCalendarByDateAsync(DateTime day, CancellationToken tokenCancel);
    }
}
