using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendingTracker.Server.Commands;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public SubcategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubcategory([FromBody] Subcategory subcategory)
        {
            var result = await _mediator.Send(new AddSubcategoryCommand(subcategory));
            return Ok(result);
        }
    }
}