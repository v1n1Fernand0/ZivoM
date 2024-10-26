namespace ZivoM.Transactions
{
    public class CreateUpdateTransactionDTO
    {
        public decimal Amount { get; set; }
        public Guid CategoryId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
