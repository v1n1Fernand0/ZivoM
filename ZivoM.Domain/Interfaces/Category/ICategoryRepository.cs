using System.Linq.Expressions;

namespace ZivoM.Categories
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllByUserIdAsync(Guid userId);
        Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}
