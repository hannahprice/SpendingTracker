﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendingTracker.Server.Commands;
using SpendingTracker.Server.Queries;
using SpendingTracker.Shared.Models;

namespace SpendingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var result = await _mediator.Send(new GetTransactionsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            var result = await _mediator.Send(new AddTransactionCommand(transaction));
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTransaction([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetTransactionQuery(id));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteTransactionCommand(id));
            return NoContent();
        }
    }
}