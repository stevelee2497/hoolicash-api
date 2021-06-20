using HooliCash.API.Extensions;
using HooliCash.DTOs.Wallet;
using HooliCash.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HooliCash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(WalletDto), StatusCodes.Status200OK)]
        public ActionResult CreateWallet([FromBody] CreateWalletDto createWalletDto)
        {
            var response = _walletService.CreateWallet(createWalletDto);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<WalletDto>), StatusCodes.Status200OK)]
        public ActionResult GetWallets()
        {
            var userId = User.GetUserId();
            var response = _walletService.GetWallets(userId);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(WalletDto), StatusCodes.Status200OK)]
        public ActionResult GetWallet(Guid id)
        {
            var response = _walletService.GetWallet(id);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(WalletDto), StatusCodes.Status200OK)]
        public ActionResult UpdateWallet([FromBody] UpdateWalletDto updateWalletDto)
        {
            var response = _walletService.UpdateWallet(updateWalletDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(WalletDto), StatusCodes.Status200OK)]
        public ActionResult DeleteWallet(Guid id)
        {
            var response = _walletService.DeleteWallet(id);
            return Ok(response);
        }
    }
}
