﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendingTracker.Server.Commands;
using SpendingTracker.Server.Queries;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubcategory([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSubcategoryQuery(id));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSubcategory([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteSubcategoryCommand(id));
            return Ok(result);
        }
    }
}