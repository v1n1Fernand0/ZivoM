using ZivoM.Domain.Constants;
using ZivoM.Entities;
using ZivoM.GenericsExceptions;

namespace ZivoM.Categories
{
    public class Category : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }

        public Category(string name, string description, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainValidationException(CategoryValidationMessages.CategoryNameIsRequired);

            if(name.Length > 0 && name.Length <= 2)
                throw new DomainValidationException(CategoryValidationMessages.CategoryNameMustBeGreaterThanTwoCharacters);

            if (userId == Guid.Empty)
                throw new DomainValidationException(CategoryValidationMessages.CategoryMustToBeAssociatedWithUser);

            Name = name;
            Description = description;
            UserId = userId;
        }

        public void UpdateCategory(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainValidationException(CategoryValidationMessages.CategoryNameIsRequired);

            Name = name;
            Description = description;

            Update();
        }
    }
}
