using DiceServiceApplication.Exceptions.Handlers;
using DiceServiceApplication.Exceptions.Handlers.Base;
using DiceServiceApplication.Exceptions.Handlers.Base.Interfaces;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Kafka.Serialization;
using DiceServiceApplication.Services;
using DiceServiceApplication.Services.Interfaces;
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

    public static IServiceCollection AddKafkaLogConfiguration(this IServiceCollection services)
    {
        return services.AddSingleton<IKafkaConfiguration, KafkaLogConfiguration>();
    }
    
    public static IServiceCollection AddKafkaHistoryConfiguration(this IServiceCollection services)
    {
        return services.AddSingleton<IKafkaConfiguration, KafkaHistoryConfiguration>();
    }

    public static IServiceCollection AddLogServices(this IServiceCollection services)
    {
        return services.AddSingleton<IAggregationService, LogAggregationService>().AddSingleton<LogService>().AddSingleton<SessionService>()
            .AddSingleton<SimulationService>();
    }
    
    public static IServiceCollection AddHistoryServices(this IServiceCollection services)
    {
        return services.AddSingleton<IAggregationService, HistoryAggregationService>().AddSingleton<HistoryService>()
            .AddSingleton<SimulationService>();
    }

    public static IServiceCollection AddUtils(this IServiceCollection services)
    {
        return services.AddSingleton<ConsumerFabric>().AddSingleton<ProducerFabric>();
    }

    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        return services.AddSingleton<IExceptionHandlerCollection, ExceptionHandlers>()
            .AddSingleton<AppExceptionHandler>();
    }

    public static IServiceCollection AddSerializers(this IServiceCollection services)
    {
        return services.AddSingleton<HistoryEntrySerializer>().AddSingleton<HistoryEntryDeserializer>();
    }
}