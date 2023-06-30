using Microsoft.AspNetCore.Mvc;
using webapi.Dto.Wallet;
using webapi.Models;

namespace webapi.Interfaces
{
    public interface IWalletInterface
    {
        WalletDto GetById(int userId, int walletId);
        ActionResult<IEnumerable<WalletDto>> GetAll(int userId);
        int Create(int userId, CreateWalletDto dto);
        void Delete(int walletId, int userId);

        void Update(int walletId, int userId, UpdateWalletDto dto);

         
    }
}
