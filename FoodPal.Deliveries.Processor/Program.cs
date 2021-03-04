using FluentValidation;
using FoodPal.Deliveries.Application.Handlers;
using FoodPal.Deliveries.Common.Settings;
using FoodPal.Deliveries.Data;
using FoodPal.Deliveries.Data.Abstractions;
using FoodPal.Deliveries.Mappers;
using FoodPal.Deliveries.Messages;
using FoodPal.Deliveries.Validations;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Processor
{
    class Program
    {
        static IConfiguration Configuration;
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext hostBuilder, IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(hostBuilder.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddUserSecrets<Program>();

            Configuration = configurationBuilder.Build();
        }

        private static void ConfigureServices(HostBuilderContext hostBuilder, IServiceCollection services)
        {
            var messageBrokerSettings = Configuration.GetSection("MessageBroker").Get<MessageBrokerSettings>();

            services.AddHostedService<MassTransitConsoleHostedService>();

            services.AddValidatorsFromAssembly(typeof(InternalValidator<>).Assembly);

            services.AddAutoMapper(typeof(InternalProfile).Assembly);
            services.AddMediatR(typeof(NewUserAddedCommandHandler).Assembly);
            services.AddMediatR(typeof(UserUpdatedCommandHandler).Assembly);
            services.AddMediatR(typeof(NewDeliveryAddedCommandHandler).Assembly);
            services.AddMediatR(typeof(DeliveryCompletedCommandHandler).Assembly);
            services.AddMediatR(typeof(UserDeliveriesRequestedQueryHandler).Assembly);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<DbSettings>(hostBuilder.Configuration.GetSection("ConnectionStrings"));
            services.AddScoped<DeliveryDbContext>();

            services.AddScoped<NewUserAddedConsumer>();
            services.AddScoped<UserUpdatedConsumer>();
            services.AddScoped<NewDeliveryAddedConsumer>();
            services.AddScoped<DeliveryCompletedConsumer>();
            services.AddScoped<UserDeliveriesRequestedConsumer>();

            services.AddMassTransit(configuration => {
                configuration.UsingAzureServiceBus((context, config) =>
                {
                    config.Host(messageBrokerSettings.ServiceBusHost);

                    config.ReceiveEndpoint("delivery-users-queue", e =>
                    {
                        // register consumer 
                        e.Consumer(() => context.GetService<NewUserAddedConsumer>());
                        e.Consumer(() => context.GetService<UserUpdatedConsumer>());
                    });

                    config.ReceiveEndpoint("delivery-queue", e =>
                    {
                        // register consumer 
                        e.Consumer(() => context.GetService<NewDeliveryAddedConsumer>());
                        e.Consumer(() => context.GetService<DeliveryCompletedConsumer>());
                        e.Consumer(() => context.GetService<UserDeliveriesRequestedConsumer>());
                    });
                });
            });

        }
    }
}
