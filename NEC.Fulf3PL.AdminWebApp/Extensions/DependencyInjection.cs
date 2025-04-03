namespace NEC.Fulf3PL.AdminWebApp.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            // configure services here
            return services;
        }
    }
}
