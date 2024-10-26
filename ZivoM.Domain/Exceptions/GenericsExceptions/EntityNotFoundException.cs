namespace ZivoM.Domain.Exceptions.GenericsExceptions
{
    public class EntityNotFoundException :Exception
    {
        public EntityNotFoundException(string entityName, Guid entityId)
            : base($"The entity '{entityName}' with Id '{entityId}' was not found.")
        {

        }
    }
}
