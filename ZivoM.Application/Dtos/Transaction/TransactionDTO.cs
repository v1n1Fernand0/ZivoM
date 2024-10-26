namespace ZivoM.Transactions
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid UserId { get; set; }
    }
}
