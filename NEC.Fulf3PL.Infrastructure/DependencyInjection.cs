using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using NEC.Fulf3PL.Application.Admin.Services;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Queries;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Options;
using NEC.Fulf3PL.Infrastructure.Persistence.CosmosDB.Serializer;
using NEC.Fulf3PL.Application.Common;
using NEC.Fulf3PL.Infrastructure.Services;
using NEC.Fulf3PL.Application.Common.KTN.Interface;
using NEC.Fulf3PL.Application.Common.KTN;
using NEC.Fulf3PL.Core.Interface;
using NEC.Fulf3PL.Infrastructure.Repositories;
using NEC.Fulf3PL.Core.Contact.Admin;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;
using NEC.Fulf3PL.Application.Common.DurableContext;
using NEC.Fulf3PL.Infrastructure.Common.Context;
using NEC.Fulf3PL.Application.Common.EmailSenderService;
using NEC.Fulf3PL.Infrastructure.EmailSenderService;
using NEC.Fulf3PL.Application.Admin.Services.ExcelGenerateService;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.ApiReportService;
using NEC.Fulf3PL.Application.Admin.Options;
using NEC.Fulf3PL.Infrastructure.EmailSenderService.Options;
using Microsoft.Extensions.Logging;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN.DTO;
using NEC.Fulf3PL.Application.Outbound.Implementation.KTN;
using NEC.Fulf3PL.Application.Outbound.Interface;
using NEC.Fulf3PL.Application.Outbound.Implementation.GEODIS;
using NEC.Fulf3PL.Application.Outbound.Interface.COMMON;
using GeodisCommonModel = NEC.Fulf3PL.Application.Outbound.Implementation.COMMON.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCosmosDbContext(configuration)
            .AddQueryServices()
            .AddDurableContext()
            .EmailService();
    }

    private static IServiceCollection AddCosmosDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var configSection = configuration.GetSection(CosmosDbConfigOptions.SectionName);
        var connectionString = configSection.Get<CosmosDbConfigOptions>()?.ConnectionString;

        var socketsHttpHandler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        };

        services.AddSingleton(socketsHttpHandler);

        services.Configure<CosmosDbConfigOptions>(configSection);

        services.AddSingleton(serviceProvider =>
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = { new StringEnumConverter() }
            };

            var cosmosClientOptions = new CosmosClientOptions()
            {
                Serializer = new NewtonsoftLinqSerializer(jsonSettings),
                HttpClientFactory = () => new HttpClient(socketsHttpHandler, disposeHandler: false)
            };

            return new CosmosClient(connectionString);
        });

        return services;
    }

    private static IServiceCollection AddQueryServices(this IServiceCollection services)
    {
        return services
            .AddTransient<ISapTransactionsQueryService, SapTransactionsQueryService>()
            .AddTransient<IAdminOutboundRequestsQueryService, AdminOutboundRquestsQueryService>()
            .AddTransient<IItemMasterQueryService, ItemMasterQueryService>();
    }

    private static IServiceCollection AddRetriggerDocumentService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpClient<IRetriggerDocumentService, KtnFunctionsRestClient>();

        return services
            .Configure<AdminRetriggerServiceOptions>(configuration.GetSection(AdminRetriggerServiceOptions.SectionName));
    }

    private static IServiceCollection AddDurableContext(this IServiceCollection services)
    {
        return services
             .AddTransient<IDurableContextManager, DurableContextManager>()
             .AddTransient<IDurableContextAccessor, DurableContextManager>();
    }

    private static IServiceCollection EmailService(this IServiceCollection services)
    {
        return services
             .AddTransient<IEmailService, EmailService>();
    }

    private static void AddEmailNotificationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailNotificationOptions>(options => configuration.GetSection(EmailNotificationOptions.SectionName));
        var emilConfigSection = configuration.GetSection(EmailNotificationOptions.SectionName);
        services.Configure<EmailNotificationOptions>(emilConfigSection);


        services.Configure<SmtpOptions>(options => configuration.GetSection(SmtpOptions.SectionName));
        var smtpConfigSection = configuration.GetSection(SmtpOptions.SectionName);
        services.Configure<SmtpOptions>(smtpConfigSection);
    }

    public static IServiceCollection AddKtnApplicationInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISpreadSheetService, KtnSpreadSheetServices>();
        return services;
    }

    public static IServiceCollection AddApplicationInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRetriggerDocumentService(configuration);
        services.AddInboundServiceBusQueuesOptions(configuration);
        services.AddTransient<IApiReportSpreadsheetService, ApiReportSpreadsheetService>()
                .AddTransient<IOutboundDocumentService, OutboundDocumentService>()
                .AddScoped<IInboundServiceBusQueueService, InboundServiceBusQueueService>()

                .AddTransient<IAdminOutboundRequestsQueryService, AdminOutboundRquestsQueryService>();

        services.AddTransient<IAdminOutboundRequestsDocumentRepository, AdminOutboundRequestsDocumentRepository>()
                .AddTransient<IAdminSapTransactionLogRepository, AdminSapTransactionLogRepository>()
                .AddTransient<IInboundSubscriberQueryService, InboundSubscriberQueryService>();

        services.AddSingleton<IInboundSubscriberRepository>(options =>
        {
            CosmosClient cosmosClient = new CosmosClient(string.Empty);

            return new InboundSubscriberRepository(cosmosClient, string.Empty, string.Empty);
        });

        services.AddSingleton<ISubscriptionExtensionLogRepository>(options =>
        {
            CosmosClient cosmosClient = new CosmosClient(string.Empty);

            return new SubscriptionExtensionLogRepository(cosmosClient, string.Empty, string.Empty);
        });

        services.AddSingleton<IOutboundTransactionRepository>(options =>
        {
            CosmosClient cosmosClient = new CosmosClient(string.Empty);
            return new OutboundTransactionRepository(cosmosClient, string.Empty, string.Empty);
        });

        services.AddScoped<IWebhookSubscriptionFactory, WebhookSubscriptionFactory>();

        services.AddTransient<IItemMasterService<OutboundConverterModel>>(options =>
        {
            return new ItemMasterService(null, configuration, null, null, null);
        });
        services.AddTransient<IItemMasterServiceCommon<GeodisCommonModel.OutboundConverterModel>>(options =>
        {
            return new ItemMasterServiceCommon(null, configuration, null, null, null);
        });


        return services;
    }
    public static IServiceCollection AddSubscriberServiceInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEmailNotificationOptions(configuration);

        services.AddTransient<IApiReportSpreadsheetService, ApiReportSpreadsheetService>();

        services.AddTransient<IAdminSapTransactionLogRepository, AdminSapTransactionLogRepository>()
                .AddTransient<IInboundSubscriberQueryService, InboundSubscriberQueryService>();

        services.AddSingleton<IOutboundTransactionRepository>(options =>
        {
            CosmosClient cosmosClient = new CosmosClient(string.Empty);
            return new OutboundTransactionRepository(cosmosClient, string.Empty, string.Empty);
        });

        return services;
    }

    public static IServiceCollection AddDeliveryInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEmailNotificationOptions(configuration);

        services.AddTransient<IAdminOutboundRequestsQueryService, AdminOutboundRquestsQueryService>();

        services.AddTransient<IAdminOutboundRequestsDocumentRepository, AdminOutboundRequestsDocumentRepository>()
                .AddTransient<IOutboundDocumentService, OutboundDocumentService>();


        services.AddSingleton<IInboundSubscriberRepository>(options =>
        {
            CosmosClient cosmosClient = new CosmosClient(string.Empty);

            return new InboundSubscriberRepository(cosmosClient, string.Empty, string.Empty);
        });
        services.AddScoped<IWebhookSubscriptionFactory, WebhookSubscriptionFactory>();

        services.AddSingleton<ISubscriptionExtensionLogRepository>(options =>
        {
            CosmosClient cosmosClient = new CosmosClient(string.Empty);

            return new SubscriptionExtensionLogRepository(cosmosClient, string.Empty, string.Empty);
        });
        return services;
    }
    private static void AddInboundServiceBusQueuesOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
             .Configure<InboundServiceBusQueuesOptions>(configuration.GetSection(InboundServiceBusQueuesOptions.SectionName));
    }
}
