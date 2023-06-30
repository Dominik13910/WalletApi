using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using webapi.Dto.Users;
using webapi.Dto.Wallet;
using webapi.Interfaces;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/User/{userId}/Wallet")]
    [ApiController]
    [Authorize (Roles = "User")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletInterface _walletServices;

        public WalletController(IWalletInterface walleServices)

        {

          _walletServices = walleServices;

        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll([FromRoute] int userId)
        {
            var wallet = _walletServices.GetAll(userId);

            return Ok(wallet);
        }

        [HttpGet("{walletId}")]
        public ActionResult<WalletDto> Get( [FromRoute] int userId, [FromRoute] int walletId)
        {
            WalletDto wallet = _walletServices.GetById(userId, walletId);

            return Ok(wallet);

        }

        [HttpPost]
        public ActionResult Post([FromRoute] int userId, [FromBody] CreateWalletDto dto)
        {
            var newWallet = _walletServices.Create(userId, dto);

            return Created($"/api/User/{userId}/Wallet/{newWallet}", null);
        }

        [HttpDelete("{walletId}")]
        public ActionResult Delete([FromRoute] int userId,[FromRoute] int walletId)
        {
            _walletServices.Delete(userId, walletId);

            return NotFound();
        }

        [HttpPut("{walletId}")]
        public ActionResult Update([FromRoute] int userId, [FromRoute] int walletId, [FromBody] UpdateWalletDto dto)
        {
            _walletServices.Update(userId, walletId, dto);

            return Ok();
        }

    }

}
