using AN.Application;
using AN.Application.Orders.Create;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace AN.WebApi.Extensions;

public static class RebusExtensions
{
    public static IServiceCollection AddRebusAndSaga(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRebus(rebus =>
                rebus
                    .Routing(r => r.TypeBased().MapAssemblyOf<ApplicationAssemblyReference>("an-queue"))
                    .Transport(t =>
                    {
                        t.UseRabbitMq(configuration.GetConnectionString("MessageBroker"), "an-queue");
                    })
                    .Sagas(s => s.StoreInPostgres(configuration.GetConnectionString(AN.Infrastructure.Constants.DatabaseConnectionStringKey), "sagas", "saga_indexes")),
            onCreated: async bus =>
            {
                await bus.Subscribe<OrderConfirmationEmailSent>();
                await bus.Subscribe<OrderPaymentRequestSent>();
            });

        services.AutoRegisterHandlersFromAssemblyOf<ApplicationAssemblyReference>();

        return services;
    }
}