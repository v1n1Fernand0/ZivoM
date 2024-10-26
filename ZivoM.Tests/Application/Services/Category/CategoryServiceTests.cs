using AutoMapper;
using Moq;
using ZivoM.Domain.Constants;

namespace ZivoM.Categories
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ICategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateCategoryAsync_Should_Create_Category_When_Valid()
        {
            var dto = new CreateUpdateCategoryDTO
            {
                Name = "Groceries",
                Description = "Daily groceries",
                UserId = Guid.NewGuid()
            };
            var category = new Category(dto.Name, dto.Description, dto.UserId);

            _mapperMock.Setup(m => m.Map<Category>(dto)).Returns(category);
            _mapperMock.Setup(m => m.Map<CategoryDTO>(category)).Returns(new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                UserId = category.UserId
            });

            var result = await _categoryService.CreateCategoryAsync(dto);

            _categoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Category>()), Times.Once);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
        }

        [Fact]
        public async Task UpdateCategoryAsync_Should_Throw_Exception_When_Category_Not_Found()
        {
            var dto = new CreateUpdateCategoryDTO
            {
                Name = "Updated Name",
                Description = "Updated Description"
            };
            var categoryId = Guid.NewGuid();

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync((Category?)null);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _categoryService.UpdateCategoryAsync(categoryId, dto));

            Assert.Equal(CategoryValidationMessages.CategoryNotFound, exception.Message);
        }

        [Fact]
        public async Task UpdateCategoryAsync_Should_Update_Category_When_Valid()
        {
            var dto = new CreateUpdateCategoryDTO
            {
                Name = "Updated Name",
                Description = "Updated Description"
            };
            var categoryId = Guid.NewGuid();
            var existingCategory = new Category("Old Name", "Old Description", Guid.NewGuid());

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(categoryId)).ReturnsAsync(existingCategory);
            _mapperMock.Setup(m => m.Map<CategoryDTO>(existingCategory)).Returns(new CategoryDTO
            {
                Id = categoryId,
                Name = dto.Name,
                Description = dto.Description,
                UserId = existingCategory.UserId
            });

            var result = await _categoryService.UpdateCategoryAsync(categoryId, dto);

            _categoryRepositoryMock.Verify(r => r.UpdateAsync(existingCategory), Times.Once);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
        }
    }
}
