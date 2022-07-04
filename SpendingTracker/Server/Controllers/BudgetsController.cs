using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendingTracker.Server.Commands;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

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

        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] Budget budget)
        {
            var result = await _mediator.Send(new AddBudgetCommand(budget));
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBudget([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetBudgetQuery(id));
            return Ok(result);
        }
    }
}