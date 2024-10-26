using ZivoM.Domain.Exceptions.GenericsExceptions;

namespace ZivoM.Categories
{
    public class CategoryNotFoundException : EntityNotFoundException
    {
        public CategoryNotFoundException(Guid entityId)
            : base("Category", entityId)
        {
        }
    }
}
