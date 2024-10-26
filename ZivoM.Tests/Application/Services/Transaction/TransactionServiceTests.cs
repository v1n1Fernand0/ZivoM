using Moq;
using ZivoM.Interfaces;
using AutoMapper;
using ZivoM.Constants;

namespace ZivoM.Transactions
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ITransactionService _transactionService;

        public TransactionServiceTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _mapperMock = new Mock<IMapper>();
            _transactionService = new TransactionService(_transactionRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateTransactionAsync_Should_Create_Transaction_When_Valid()
        {
            var dto = new CreateUpdateTransactionDTO
            {
                Amount = 100,
                Date = DateTimeOffset.UtcNow,
                CategoryId = Guid.NewGuid(),
                Description = "Groceries",
                UserId = Guid.NewGuid()
            };
            var transaction = new Transaction(dto.Amount, dto.Date, dto.CategoryId, dto.Description, dto.UserId);

            _mapperMock.Setup(m => m.Map<Transaction>(dto)).Returns(transaction);
            _mapperMock.Setup(m => m.Map<TransactionDTO>(transaction)).Returns(new TransactionDTO
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Description = transaction.Description,
                Date = transaction.Date,
                UserId = transaction.UserId
            });

            var result = await _transactionService.CreateTransactionAsync(dto);

            _transactionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
            Assert.Equal(transaction.Amount, result.Amount);
            Assert.Equal(transaction.Description, result.Description);
        }

        [Fact]
        public async Task UpdateTransactionAsync_Should_Throw_Exception_When_Transaction_Not_Found()
        {
            var dto = new CreateUpdateTransactionDTO
            {
                Amount = 150,
                Date = DateTimeOffset.UtcNow,
                CategoryId = Guid.NewGuid(),
                Description = "Updated Description"
            };
            var transactionId = Guid.NewGuid();

            _transactionRepositoryMock.Setup(r => r.GetByIdAsync(transactionId)).ReturnsAsync((Transaction)null);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transactionService.UpdateTransactionAsync(transactionId, dto));

            Assert.Equal(TransactionValidationMessages.TransactionNotFound, exception.Message);
        }

        [Fact]
        public async Task UpdateTransactionAsync_Should_Update_Transaction_When_Valid()
        {
            var dto = new CreateUpdateTransactionDTO
            {
                Amount = 200,
                Date = DateTimeOffset.UtcNow,
                CategoryId = Guid.NewGuid(),
                Description = "Updated Description"
            };
            var transactionId = Guid.NewGuid();
            var existingTransaction = new Transaction(100, DateTimeOffset.UtcNow, Guid.NewGuid(), "Old Description", Guid.NewGuid());

            _transactionRepositoryMock.Setup(r => r.GetByIdAsync(transactionId)).ReturnsAsync(existingTransaction);
            _mapperMock.Setup(m => m.Map<TransactionDTO>(existingTransaction)).Returns(new TransactionDTO
            {
                Id = transactionId,
                Amount = dto.Amount,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Date = dto.Date,
                UserId = existingTransaction.UserId
            });

            var result = await _transactionService.UpdateTransactionAsync(transactionId, dto);

            _transactionRepositoryMock.Verify(r => r.UpdateAsync(existingTransaction), Times.Once);
            Assert.Equal(dto.Amount, result.Amount);
            Assert.Equal(dto.Description, result.Description);
        }
    }
}
