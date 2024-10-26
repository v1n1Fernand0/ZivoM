using ZivoM.GenericsExceptions;

namespace ZivoM.Transactions
{
    public class TransactionTests
    {
        [Fact]
        public void Should_Create_Transaction_With_Valid_Data()
        {
            var amount = 100m;
            var date = DateTimeOffset.UtcNow;
            var categoryId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var description = "Transaction 1";

            var transaction = new Transaction(amount, date, categoryId, description, userId);

            Assert.NotNull(transaction);
            Assert.Equal("Transaction 1", transaction.Description);
            Assert.Equal(amount, transaction.Amount);
            Assert.Equal(date, transaction.Date);
            Assert.Equal(categoryId, transaction.CategoryId);
            Assert.Equal(userId, transaction.UserId);
            Assert.Equal(description, transaction.Description);
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_Amount_Is_Less_Than_Or_Equal_To_Zero()
        {
            var amount = 0m;
            var date = DateTimeOffset.UtcNow;
            var categoryId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var description = "Transaction 1";

            Assert.Throws<DomainValidationException>(() => new Transaction(amount, date, categoryId, description, userId));
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_Date_Is_In_The_Future()
        {
            var amount = 100m;
            var date = DateTimeOffset.UtcNow.AddDays(1);
            var categoryId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var description = "Transaction 1";

            Assert.Throws<DomainValidationException>(() => new Transaction(amount, date, categoryId, description, userId));
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_CategoryId_Is_Empty()
        {
            var amount = 100m;
            var date = DateTimeOffset.UtcNow;
            var categoryId = Guid.Empty;
            var userId = Guid.NewGuid();
            var description = "Transaction 1";

            Assert.Throws<DomainValidationException>(() => new Transaction(amount, date, categoryId, description, userId));
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_UserId_Is_Empty()
        {
            var amount = 100m;
            var date = DateTimeOffset.UtcNow;
            var categoryId = Guid.NewGuid();
            var userId = Guid.Empty;
            var description = "Transaction 1";

            Assert.Throws<DomainValidationException>(() => new Transaction(amount, date, categoryId, description, userId));
        }
    }
}
