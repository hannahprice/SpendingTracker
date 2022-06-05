﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendingTracker.Server.Queries;

namespace SpendingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var result = await _mediator.Send(new GetTransactionsQuery());
            return Ok(result);
        }
    }
}
