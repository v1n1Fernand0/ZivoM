namespace ZivoM.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private  set; }
        public bool IsDeleted { get; private set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
            IsDeleted = false;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Update()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
