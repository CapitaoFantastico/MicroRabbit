namespace MicroRabbit.Banking.Application.Models
{
    public class AccountTransfer
    {
        public int AccountFrom { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
