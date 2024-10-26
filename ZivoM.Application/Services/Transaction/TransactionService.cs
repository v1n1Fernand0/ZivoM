using AutoMapper;
using ZivoM.Constants;
using ZivoM.Interfaces;

namespace ZivoM.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionDTO> CreateTransactionAsync(CreateUpdateTransactionDTO dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);

            await _transactionRepository.AddAsync(transaction);

            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<TransactionDTO> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id)
                ?? throw new InvalidOperationException(TransactionValidationMessages.TransactionNotFound);

            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAsync(Guid userId)
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public async Task<TransactionDTO> UpdateTransactionAsync(Guid id, CreateUpdateTransactionDTO dto)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id)
                ?? throw new InvalidOperationException(TransactionValidationMessages.TransactionNotFound);

            transaction.UpdateTransaction(dto.Amount, dto.Date, dto.CategoryId, dto.Description);

            await _transactionRepository.UpdateAsync(transaction);

            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            await _transactionRepository.DeleteAsync(id);
        }
    }
}
