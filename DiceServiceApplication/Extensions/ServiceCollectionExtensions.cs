using DiceServiceApplication.Exceptions.Handlers;
using DiceServiceApplication.Exceptions.Handlers.Base;
using DiceServiceApplication.Exceptions.Handlers.Base.Interfaces;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Services;
using DiceServiceApplication.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiceServiceApplication.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        return services.AddLogging(logging => logging.AddConsole());
    }

    public static IServiceCollection AddKafkaHandler(this IServiceCollection services)
    {
        return services.AddSingleton<IKafkaHandler<string, string>, KafkaMessageHandler<string, string>>();
    }

    public static IServiceCollection AddKafkaConfiguration(this IServiceCollection services)
    {
        return services.AddSingleton<KafkaConfiguration>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddSingleton<LogAggregationService>().AddSingleton<LogService>().AddSingleton<SessionService>()
            .AddSingleton<SimulationService>();
    }

    public static IServiceCollection AddUtils(this IServiceCollection services)
    {
        return services.AddSingleton<ConsumerUtils>().AddSingleton<ProducerUtils>();
    }

    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        return services.AddSingleton<IExceptionHandlerCollection, ExceptionHandlers>()
            .AddSingleton<AppExceptionHandler>();
    }
}