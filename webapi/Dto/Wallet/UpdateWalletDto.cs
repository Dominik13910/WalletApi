namespace webapi.Dto.Wallet
{
    public class UpdateWalletDto
    {
        public int WalletId { get; set; }
        public decimal Pln { get; set; }
        public decimal BitCoin { get; set; }
        public decimal Ether { get; set; }
    }
}
