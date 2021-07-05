using HooliCash.API.Extensions;
using HooliCash.DTOs.Transaction;
using HooliCash.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace HooliCash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        public ActionResult CreateTransaction([FromBody] CreateTransactionDto createTransactionDto)
        {
            var response = _transactionService.CreateTransaction(createTransactionDto);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
        public ActionResult GetTransactions([FromQuery] TransactionQuery transactionQuery)
        {
            var response = _transactionService.GetTransactions(transactionQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        public ActionResult GetTransaction(Guid id)
        {
            var response = _transactionService.GetTransaction(id);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        public ActionResult UpdateTransaction([FromBody] UpdateTransactionDto updateTransactionDto)
        {
            var response = _transactionService.UpdateTransaction(updateTransactionDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        public ActionResult DeleteTransaction(Guid id)
        {
            var response = _transactionService.DeleteTransaction(id);
            return Ok(response);
        }

        [HttpPost("import")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
        public ActionResult ImportTransactions([FromForm] UploadFile dto)
        {
            using var reader = new StreamReader(dto.File.OpenReadStream());
            var userId = User.GetUserId();
            var response = _transactionService.ImportTransactions(reader, userId);
            return Ok(response);
        }
    }

    public class UploadFile
    {
        public IFormFile File { get; set; }
    }
}
