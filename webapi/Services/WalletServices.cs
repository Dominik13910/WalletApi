using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using webapi.Authentications;
using webapi.Dto.Users;
using webapi.Dto.Wallet;
using webapi.Exceptios;
using webapi.Interfaces;
using webapi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;


namespace webapi.Services
{
    public class WalletServices : IWalletInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
       
        public WalletServices(AppDbContext dbContext, IMapper mapper, ILogger<UserServices> logger)
        {
            _appDbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
           
        }

        public WalletDto GetById(int userId, int walletId)
        {
            var user = GetUserById(userId);
            var wallet = GetWalletById(walletId);
            var result = _mapper.Map<WalletDto>(wallet);
            return result;
        }

        public ActionResult<IEnumerable<WalletDto>> GetAll(int userId)
        {
            var user = GetUserById(userId);
            var wallet = _appDbContext
             .Wallet
             .ToList();
            var usersDto = _mapper.Map<List<WalletDto>>(wallet);
            return usersDto;
        }

        public int Create(int userId, CreateWalletDto dto)
        {
            var user = GetUserById(userId);
            var wallet = _mapper.Map<Wallet>(dto);
            wallet.UserId = userId;
            _appDbContext.Wallet.Add(wallet);
            _appDbContext.SaveChanges();

            return wallet.WalletId;
        }

        public void Delete(int userId, int walletId)
        {
            _logger.LogError($"User with id: {walletId} Deleted action invoked");
            var user = GetUserById(userId);
            var wallet = GetWalletById(walletId);
            _appDbContext.Wallet.Remove(wallet);
            _appDbContext.SaveChanges();
        }

        public void Update(int userId, int walletId, UpdateWalletDto dto)
        {
            var user = GetUserById(userId);
            var wallet = GetWalletById(walletId);
            wallet.Pln = dto.Pln;
            wallet.BitCoin = dto.BitCoin;
            wallet.Ether = dto.Ether;
            _appDbContext.SaveChanges();
        }

        private User GetUserById(int userId)
        {
            var user = _appDbContext
                .User
                .Include(u => u.Wallet)
                .FirstOrDefault(u => u.Id == userId);

            if (user is null)
                throw new NotFoundException("Role not found");

            return user;
        }

        private Wallet GetWalletById(int walletId)
        {
            var wallet = _appDbContext
             .Wallet
             .FirstOrDefault(u => u.WalletId == walletId);
            if (wallet is null)
                throw new NotFoundException("User not found");
            return wallet;
        }
    }
}
