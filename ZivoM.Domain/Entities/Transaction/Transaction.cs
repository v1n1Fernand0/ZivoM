using ZivoM.Constants;
using ZivoM.Entities;
using ZivoM.GenericsExceptions;

namespace ZivoM.Transactions
{
    public class Transaction : EntityBase
    {
        public decimal Amount { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }

        public Transaction(decimal amount, DateTimeOffset date, Guid categoryId, string description, Guid userId)
        {
            if (amount <= 0)
                throw new DomainValidationException(TransactionValidationMessages.AmountMustBeGreaterThanZero);

            if (date > DateTimeOffset.UtcNow)
                throw new DomainValidationException(TransactionValidationMessages.DateCannotBeInTheFuture);

            if (categoryId == Guid.Empty)
                throw new DomainValidationException(TransactionValidationMessages.CategoryIdIsRequired);

            if (userId == Guid.Empty)
                throw new DomainValidationException(TransactionValidationMessages.UserIdIsRequired);

            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
            UserId = userId;
        }

        public void UpdateTransaction(decimal amount, DateTimeOffset date, Guid categoryId, string description)
        {
            if (amount <= 0)
                throw new DomainValidationException(TransactionValidationMessages.AmountMustBeGreaterThanZero);

            if (date > DateTimeOffset.UtcNow)
                throw new DomainValidationException(TransactionValidationMessages.DateCannotBeInTheFuture);

            if (categoryId == Guid.Empty)
                throw new DomainValidationException(TransactionValidationMessages.CategoryIdIsRequired);

            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;

            Update();
        }
    }
}
