using Mc2.CrudTest.ServerHelper.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Mc2.CrudTest.ServerHelper.IoC
{
    public static class ServiceCollectionExtension
    {
        public static void AddApiClientService(this IServiceCollection services,
            Action<ApiClientOptions> options)
        {
            services.Configure(options);
            services.AddSingleton(provider =>
            {
                ApiClientOptions opt = provider.GetRequiredService<IOptions<ApiClientOptions>>().Value;
                return new CrudTestClientService(opt);
            });
        }
    }
}
