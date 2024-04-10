using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ApiGateway.Helpers
{
    public static class DependencyInjection
    {
        public static IServiceCollection ServiceDescriptors(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var ocelotSettings = new OcelotSettings();
            configurationManager.GetSection("OcelotSettings").Bind(ocelotSettings);
            if(ocelotSettings != null)
            {
                var currentDirectory = Environment.CurrentDirectory;
                foreach (var item in ocelotSettings.OcelotNames)
                {
                   
                    var filePath = Path.Combine(currentDirectory, item);

                    configurationManager.AddJsonFile(filePath, optional: false, reloadOnChange: true);
                }
            }
            return services;
        }
    }
}
