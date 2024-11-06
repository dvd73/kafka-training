using DiceServiceApplication.Domain.Models;
using DiceServiceApplication.Exceptions.Handlers;
using DiceServiceApplication.Exceptions.Handlers.Base;
using DiceServiceApplication.Exceptions.Handlers.Base.Interfaces;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Kafka.Serialization;
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

    public static IServiceCollection AddKafkaMessageHandlers(this IServiceCollection services)
    {
        return services
            .AddSingleton<IKafkaHandler<string, string>, KafkaBaseMessageHandler<string, string>>()
            .AddSingleton<IKafkaHandler<string, HistoryEntry>, KafkaHistoryEntryHandler>();
    }
    
    public static IServiceCollection AddKafkaLogConfiguration(this IServiceCollection services)
    {
        return services.AddSingleton<KafkaLogConfiguration>();
    }

    public static IServiceCollection AddKafkaHistoryConfiguration(this IServiceCollection services)
    {
        return services.AddSingleton<KafkaHistoryConfiguration>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddSingleton<LogAggregationService>()
            .AddSingleton<HistoryAggregationService>()
            .AddSingleton<LogService>()
            .AddSingleton<HistoryService>()
            .AddSingleton<SessionService>()
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