using ZivoM.GenericsExceptions;

namespace ZivoM.Categories
{
    public class CategoryTests
    {
        [Fact]
        public void Should_Create_Category_With_Valid_Name()
        {
            var name = "Category Name";
            var description = "Category Description";
            var userId = Guid.NewGuid();

            var category = new Category(name, description, userId);

            Assert.Equal(name, category.Name);
            Assert.Equal(description, category.Description);
            Assert.Equal(userId, category.UserId);
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_Name_Is_Null()
        {
            var name = string.Empty;
            var description = "Category Description";
            var userId = Guid.NewGuid();

            Assert.Throws<DomainValidationException>(() => new Category(name, description, userId));
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_Name_Is_Less_Than_Or_Equal_To_Two_Characters()
        {
            var name = "A";
            var description = "Category Description";
            var userId = Guid.NewGuid();

            Assert.Throws<DomainValidationException>(() => new Category(name, description, userId));
        }

        [Fact]
        public void Should_Throw_DomainValidationException_When_UserId_Is_Empty()
        {
            var name = "Category Name";
            var description = "Category Description";
            var userId = Guid.Empty;

            Assert.Throws<DomainValidationException>(() => new Category(name, description, userId));
        }

        [Fact]
        public void Should_Update_Category_With_Valid_Name()
        {
            var name = "Category Name";
            var description = "Category Description";
            var userId = Guid.NewGuid();

            var category = new Category(name, description, userId);

            category.UpdateCategory("Category Name Updated", "Category Description Updated");

            Assert.Equal("Category Name Updated", category.Name);
            Assert.Equal("Category Description Updated", category.Description);
        }
    }
}
