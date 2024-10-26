using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZivoM.Contexts;

namespace ZivoM.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ZivoMDbContext _dbContext;

        public CategoryRepository(ZivoMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllByUserIdAsync(Guid userId)
        {
            return await _dbContext.Categories
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _dbContext.Categories
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                category.Delete();
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
