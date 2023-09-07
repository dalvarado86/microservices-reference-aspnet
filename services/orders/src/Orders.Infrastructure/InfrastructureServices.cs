using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using Orders.Application.Abstractions;
using Orders.Application.Models;

namespace Orders.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            const string DbConnectionSection = "OrdersConnectionString";
            const string EmailSettingsSection = "EmailSettings";
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(DbConnectionSection)));

            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection(EmailSettingsSection));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
