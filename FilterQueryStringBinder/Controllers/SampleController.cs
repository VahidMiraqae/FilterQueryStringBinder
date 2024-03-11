using FilterQueryStringBinder.Binders;
using FilterQueryStringBinder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FilterQueryStringBinder.Controllers
{
    [ApiController]
    [Route("api")]
    public class SampleController : ControllerBase
    { 
        public SampleController(ILogger<SampleController> logger)
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get(ConditionSet<Student> conditionSet)
        {
            await Task.CompletedTask; 
            return Ok(); 
        }
    }
}
