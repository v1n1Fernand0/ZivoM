using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ZivoM.Accounts;
using ZivoM.Application.Services;
using ZivoM.Categories;
using ZivoM.Interfaces;
using ZivoM.Transactions;

namespace ZivoM.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
