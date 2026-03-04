using Fitness.Application.DTOs.CalendarsDTO;
using Fitness.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCalendarDTO>> Register([FromBody] CreateCalendarDTO createCalendarDTO, CancellationToken tokenCancel)
        {
            var calendarDto = await _calendarService.CreateCalendarAsync(createCalendarDTO, tokenCancel);

            return CreatedAtAction(nameof(GetCalendarById), new { id = calendarDto.Id }, calendarDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<CalendarDTO>>> GetAllCalendars(CancellationToken tokenCancel)
        {
            return Ok(await _calendarService.GetAllCalendarsAsync(tokenCancel));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCalendar(long id, [FromBody] UpdateCalendarDTO calendarDTO, CancellationToken tokenCancel)
        {
            return Ok(await _calendarService.UpdateCalendarAsync(id, calendarDTO, tokenCancel));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCalendar(long id, CancellationToken tokenCancel)
        {
            await _calendarService.DeleteCalendarAsync(id, tokenCancel);

            return NoContent();
        }

        [HttpGet]
        [Route("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCalendarById(long id, CancellationToken tokenCancel)
        {
            return Ok(await _calendarService.GetCalendarByIdAsync(id, tokenCancel));
        }
    }
}
