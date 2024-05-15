using DotNetTask.Models;
using DotNetTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly ICosmosDbServices _cosmosDbService;

        public EmployeeController(ICosmosDbServices cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpPost]
        [Route("SubmitDetail")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeModel employeeModel)
        {
            var Id = await _cosmosDbService.CreateEmployeeAsync(employeeModel);
            return CreatedAtAction(nameof(CreateEmployee), new { id = Id }, Id);
        }
    }
}
