using kafka_training.Exceptions.Handlers;
using kafka_training.Exceptions.Handlers.Base;
using kafka_training.Exceptions.Handlers.Base.Interfaces;
using kafka_training.Kafka;
using kafka_training.Services;
using kafka_training.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace kafka_training.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogger(this IServiceCollection services) =>
        services.AddLogging(logging => logging.AddConsole());
    public static IServiceCollection AddKafkaHandler(this IServiceCollection services) =>
        services.AddScoped<IKafkaHandler<string, string>, KafkaMessageHandler<string, string>>();
    
    public static IServiceCollection AddKafkaConfiguration(this IServiceCollection services) => 
        services.AddSingleton<KafkaConfiguration>();

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddScoped<LogAggregationService>().AddScoped<LogService>();

    public static IServiceCollection AddUtils(this IServiceCollection services) => 
        services.AddSingleton<ConsumerUtils>().AddSingleton<ProducerUtils>();

    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services) =>
        services.AddSingleton<IExceptionHandlerCollection, ExceptionHandlers>()
            .AddSingleton<AppExceptionHandler>();
}