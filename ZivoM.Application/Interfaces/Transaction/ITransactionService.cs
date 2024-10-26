using ZivoM.Transactions;

namespace ZivoM.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDTO> CreateTransactionAsync(CreateUpdateTransactionDTO dto);
        Task<TransactionDTO> GetTransactionByIdAsync(Guid id);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAsync(Guid userId);
        Task<TransactionDTO> UpdateTransactionAsync(Guid id, CreateUpdateTransactionDTO dto);
        Task DeleteTransactionAsync(Guid id);
    }
}
