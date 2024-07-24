using AN.Application.Behaviors;
using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace AN.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            configuration.NotificationPublisher = new TaskWhenAllPublisher();
            configuration.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });
        return services;
    }
}