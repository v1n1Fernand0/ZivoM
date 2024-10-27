using Microsoft.EntityFrameworkCore;
using ZivoM.Categories;
using ZivoM.Entities;
using ZivoM.Infrastructure.Helpers;
using ZivoM.Transactions;

namespace ZivoM.Contexts
{
    public class ZivoMDbContext : DbContext
    {
        public ZivoMDbContext(DbContextOptions<ZivoMDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(entityType => typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                .Select(entityType => entityType.ClrType);

            foreach (var clrType in entityTypes)
                modelBuilder.Entity(clrType)
                    .HasQueryFilter(EntityQueryFilters.CreateIsDeletedFilter(clrType));

        }
    }
}
