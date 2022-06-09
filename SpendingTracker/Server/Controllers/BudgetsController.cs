using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendingTracker.Server.Queries;

namespace SpendingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : Controller
    {
        private readonly IMediator _mediator;

        public BudgetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var result = await _mediator.Send(new GetBudgetsQuery());
            return Ok(result);
        }
    }
}
