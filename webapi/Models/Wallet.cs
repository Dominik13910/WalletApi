namespace webapi.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public decimal Pln { get; set; }
        public decimal BitCoin { get; set; }
        public decimal Ether { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
