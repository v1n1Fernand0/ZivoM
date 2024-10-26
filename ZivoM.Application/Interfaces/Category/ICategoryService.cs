namespace ZivoM.Categories
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateCategoryAsync(CreateUpdateCategoryDTO dto);
        Task<CategoryDTO> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<CategoryDTO>> GetCategoriesByUserIdAsync(Guid userId);
        Task<CategoryDTO> UpdateCategoryAsync(Guid id, CreateUpdateCategoryDTO dto);
        Task DeleteCategoryAsync(Guid id);
    }
}
