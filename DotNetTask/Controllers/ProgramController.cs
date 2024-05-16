using DotNetTask.Models;
using DotNetTask.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpPost]
        [Route("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionModel questionDto)
        {
            var questionId = await _programService.CreateQuestionAsync(questionDto);
            return CreatedAtAction(nameof(CreateQuestion), new { id = questionId }, questionId);
        }

        [HttpPut]
        [Route("UpdateQuestion/{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionModel questionDto)
        {
            await _programService.UpdateQuestionAsync(id, questionDto);
            return NoContent();
        }

        [HttpGet]
        [Route("GetQuestionsByType/{questionType}")]
        public async Task<IActionResult> GetQuestionsByType(int questionType)
        {
            List<QuestionModel> questions = await _programService.GetQuestionsByTypeAsync(questionType);

            if (questions == null)
                return NotFound();

            return Ok(questions);
        }
    }
}
