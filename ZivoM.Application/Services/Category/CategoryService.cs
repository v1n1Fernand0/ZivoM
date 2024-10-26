using AutoMapper;
using ZivoM.Domain.Constants;

namespace ZivoM.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateUpdateCategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);

            await _categoryRepository.AddAsync(category);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new InvalidOperationException(CategoryValidationMessages.CategoryNotFound);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByUserIdAsync(Guid userId)
        {
            var categories = await _categoryRepository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(Guid id, CreateUpdateCategoryDTO dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new InvalidOperationException(CategoryValidationMessages.CategoryNotFound);

            category.UpdateCategory(dto.Name, dto.Description);

            await _categoryRepository.UpdateAsync(category);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
