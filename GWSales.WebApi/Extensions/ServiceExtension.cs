using System.Reflection;

namespace GWSales.WebApi.Extensions;

public static class ServiceExtension
{
    public static void ValidateDependencies(
       this IServiceProvider rootServiceProvider,
       IServiceCollection services,
       IEnumerable<Assembly> assembliesToScan
   )
    {
        var exceptions = new List<string>();

        using var scope = rootServiceProvider.CreateScope();
        var sp = scope.ServiceProvider;

        foreach (var serviceDescriptor in services)
        {
            try
            {
                var serviceType = serviceDescriptor.ServiceType;
                if (assembliesToScan.Contains(serviceType.Assembly))
                {
                    sp.GetRequiredService(serviceType);
                }
            }
            catch (Exception e)
            {
                exceptions.Add($"Unable to resolve '{serviceDescriptor.ServiceType.FullName}', detail: {e.Message}");
            }
        }

        if (exceptions.Any())
        {
            throw new Exception(string.Join("\n", exceptions));
        }
    }
}
