using kafka_training.Exceptions.Handlers;
using kafka_training.Exceptions.Handlers.Base;
using kafka_training.Exceptions.Handlers.Base.Interfaces;
using kafka_training.Kafka;
using kafka_training.Services;
using kafka_training.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace kafka_training.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaHandler(this IServiceCollection services) =>
        services.AddScoped(typeof(IKafkaHandler<string, string>), typeof(KafkaMessageHandler<string, string>));
    
    public static IServiceCollection AddKafkaConfiguration(this IServiceCollection services) => 
        services.AddSingleton(new KafkaConfiguration());

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddScoped(typeof(LogAggregationService)).AddScoped(typeof(LogService));

    public static IServiceCollection AddUtils(this IServiceCollection services) => 
        services.AddScoped<ConsumerUtils>().AddScoped(typeof(ProducerUtils));

    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services) =>
        services.AddSingleton<IExceptionHandlerCollection, ExceptionHandlers>()
            .AddSingleton<AppExceptionHandler>();
}