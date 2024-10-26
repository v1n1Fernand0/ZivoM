using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZivoM.Contexts;

namespace ZivoM.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ZivoMDbContext _dbContext;

        public TransactionRepository(ZivoMDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Transactions.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Transactions
                .Where(t => t.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> FindAsync(Expression<Func<Transaction, bool>> predicate)
        {
            return await _dbContext.Transactions
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _dbContext.Transactions.Update(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var transaction = await GetByIdAsync(id);
            if (transaction != null)
            {
                transaction.Delete();
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
