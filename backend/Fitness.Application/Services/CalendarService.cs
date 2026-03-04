using AutoMapper;
using Fitness.Application.DTOs.CalendarsDTO;
using Fitness.Application.IRepositories;
using Fitness.Application.IServices;
using Fitness.Domain.Exceptions;

namespace Fitness.Application.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper _mapper;

        public CalendarService(ICalendarRepository calendarRepository, IMapper mapper)
        {
            _calendarRepository = calendarRepository;
            _mapper = mapper;
        }

        public async Task<CalendarDTO> CreateCalendarAsync(CreateCalendarDTO createCalendarDTO, CancellationToken tokenCancel)
        {
            var calendar = _mapper.Map<Domain.Models.Calendar>(createCalendarDTO);
            var existingCalendar = await _calendarRepository.GetDayAsync(day: calendar.Day, cancellationToken: tokenCancel);

            if (existingCalendar != null)
            {
                throw new BadRequestException($"Calendar with day {calendar.Day} is already exist");
            }

            await _calendarRepository.CreateAsync(entity: calendar, token: tokenCancel);

            return _mapper.Map<CalendarDTO>(calendar);
        }

        public async Task<List<CalendarDTO>> GetAllCalendarsAsync(CancellationToken tokenCancel)
        {
            var calendars = await _calendarRepository.GetAllAsync(token: tokenCancel);
            var calendarsDTOs = _mapper.Map<List<CalendarDTO>>(calendars);

            return calendarsDTOs;
        }

        public async Task<CalendarDTO> UpdateCalendarAsync(long id, UpdateCalendarDTO calendarDTO, CancellationToken tokenCancel)
        {
            if (calendarDTO.Id != id)
            {
                throw new BadRequestException("Id mismatch");
            }

            var calendar = await _calendarRepository.GetByIdAsync(id: calendarDTO.Id, token: tokenCancel)
                ?? throw new NotFoundException($"Calendar with id {calendarDTO.Id} is not found.");

            calendar = _mapper.Map(calendarDTO, calendar);
            await _calendarRepository.Update(entity: calendar, token: tokenCancel);

            return _mapper.Map<CalendarDTO>(calendar);
        }

        public async Task DeleteCalendarAsync(long id, CancellationToken tokenCancel)
        {
            var calendar = await _calendarRepository.GetByIdAsync(id: id, token: tokenCancel)
                ?? throw new NotFoundException($"Calendar with Id ({id}) is not found");

            await _calendarRepository.Delete(calendar, tokenCancel);
        }

        public async Task<CalendarDTO?> GetCalendarByIdAsync(long id, CancellationToken tokenCancel)
        {
            var calendar = await _calendarRepository.GetByIdAsync(id: id, token: tokenCancel)
                ?? throw new NotFoundException($"Calendar with Id ({id}) is not found.");

            var calendarDTO = _mapper.Map<CalendarDTO>(calendar);

            return calendarDTO;
        }

        public async Task<CalendarDTO?> GetCalendarByDateAsync(DateTime day, CancellationToken tokenCancel)
        {
            var calendar = await _calendarRepository.GetDayAsync(day: day, cancellationToken: tokenCancel)
                ?? throw new NotFoundException($"Calendar with day {day} is not found.");

            return _mapper.Map<CalendarDTO>(calendar);
        }
    }
}
