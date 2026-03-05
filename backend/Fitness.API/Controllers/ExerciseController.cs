using Fitness.Application.DTOs.ExercisesDTO;
using Fitness.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseSerivce _exerciseService;

        public ExerciseController(IExerciseSerivce exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateExerciseDTO>> CreateExercise([FromBody] CreateExerciseDTO createExerciseDTO, CancellationToken tokenCancel)
        {
            var exerciseDto = await _exerciseService.CreateExerciseAsync(createExerciseDTO, tokenCancel);

            return CreatedAtAction(nameof(GetExerciseById), new { id = exerciseDto.Id }, exerciseDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetAllExercises(CancellationToken tokenCancel)
        {
            return Ok(await _exerciseService.GetAllExercisesAsync(tokenCancel));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExercise(long id, [FromBody] UpdateExerciseDTO exerciseDTO, CancellationToken tokenCancel)
        {
            return Ok(await _exerciseService.UpdateExerciseAsync(id, exerciseDTO, tokenCancel));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteExercise(long id, CancellationToken tokenCancel)
        {
            await _exerciseService.DeleteExerciseAsync(id, tokenCancel);

            return NoContent();
        }

        [HttpGet]
        [Route("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExerciseById(long id, CancellationToken tokenCancel)
        {
            return Ok(await _exerciseService.GetExerciseByIdAsync(id, tokenCancel));
        }

        [HttpGet("{userId}/exercises")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercisesByUserAndDay(long userId, DateTime day, CancellationToken tokenCancel)
        {
            return Ok(await _exerciseService.GetExercisesByUserAndDay(userId, day, tokenCancel));
        }
    }
}
