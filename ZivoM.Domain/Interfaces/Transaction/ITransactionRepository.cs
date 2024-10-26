using System.Linq.Expressions;

namespace ZivoM.Transactions
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Transaction>> FindAsync(Expression<Func<Transaction, bool>> predicate);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Guid id);
    }
}
