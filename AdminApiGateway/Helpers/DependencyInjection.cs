using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace AdminApiGateway.Helpers
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
                   
                    var filePath = Path.Combine(currentDirectory, "OcelotJson", item);
                    configurationManager.AddJsonFile(filePath, optional: false, reloadOnChange: true);
                }
            }
            return services;
        }
    }
}
