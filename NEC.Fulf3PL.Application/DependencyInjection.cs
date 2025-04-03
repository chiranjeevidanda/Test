using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using NEC.Fulf3PL.Application.Admin.Options;
using NEC.Fulf3PL.Application.Commands;
using NEC.Fulf3PL.Application.Common.Behaviours;
using NEC.Fulf3PL.Application.Inbound.Implementation.KTN.Options;
using NEC.Fulf3PL.Infrastructure.Persistence.Admin.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(MediatorCQRS));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        return services
            .AddConfiguredOptions(configuration);
    }

    private static IServiceCollection AddConfiguredOptions(this IServiceCollection services, IConfiguration configuration)
    {    
        services.Configure<AdminOutboundCustomerOptions>(options => configuration.GetSection(AdminOutboundCustomerOptions.SectionName));

        var configSection = configuration.GetSection(AdminOutboundCustomerOptions.SectionName);

        services.Configure<AdminOutboundCustomerOptions>(configSection);

        
        services.Configure<AdminInboundCustomerOptions>(options => configuration.GetSection(AdminInboundCustomerOptions.SectionName));

        var inboundConfigSection = configuration.GetSection(AdminInboundCustomerOptions.SectionName);

        services.Configure<AdminInboundCustomerOptions>(inboundConfigSection);

        services.Configure<ExculdePlantOrderOptions>(options => configuration.GetSection(ExculdePlantOrderOptions.SectionName));


        var exculdeOutboundPlantOrderOptions = configuration.GetSection(ExculdePlantOrderOptions.SectionName);

        services.Configure<ExculdePlantOrderOptions>(exculdeOutboundPlantOrderOptions);

        var inboundReportFileOptions = configuration.GetSection(InboundReportFileOptions.SectionName);
        services.Configure<InboundReportFileOptions>(inboundReportFileOptions);

        services.Configure<CustomerPlantOptions>(options => configuration.GetSection(CustomerPlantOptions.SectionName));
        var customerPlantOrderOptions = configuration.GetSection(CustomerPlantOptions.SectionName);
        services.Configure<CustomerPlantOptions>(customerPlantOrderOptions);

        return services;
    }
}
