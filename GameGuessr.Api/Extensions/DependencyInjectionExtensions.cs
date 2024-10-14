using System.Reflection;
using GameGuessr.Api.Infrastructure.DepedencyInjection;

namespace GameGuessr.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInjectables(this IServiceCollection services)
    {
        var allTypes = AppDomain.CurrentDomain
           .GetAssemblies()
           .Where(a => a.FullName != null && a.FullName.StartsWith("GameGuessr") && a.FullName.Contains("Tests") == false)
           .SelectMany(a => a.GetTypes())
           .ToList();

        var scopedTypes = allTypes
           .Where(t => t.GetCustomAttributes(typeof(InjectableAttribute), false).Any());
        
        AddTypes(services, scopedTypes, allTypes);

        return services;
    }

    private static void AddTypes(IServiceCollection services, IEnumerable<Type> types, IReadOnlyCollection<Type> allTypes)
    {
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<InjectableAttribute>();
            
            if (type.IsClass && type.IsAbstract == false)
                AddClassType(services, attribute, type);

            if (type.IsInterface || type.IsAbstract)
                AddInterfaceType(services, allTypes, attribute, type);
        }
    }

    private static void AddClassType(IServiceCollection services, InjectableAttribute attribute, Type type)
    {
        switch (attribute.Lifetime)
        {
            case Lifetime.Transient:
                services.AddTransient(type);

                break;
            case Lifetime.Singleton:
                services.AddSingleton(type);

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attribute.Lifetime), attribute.Lifetime, null);
        }
    }

    private static void AddInterfaceType(IServiceCollection services, IEnumerable<Type> allTypes, InjectableAttribute attribute, Type type)
    {
        var implementations = allTypes
           .Where(c => c.IsClass && c.GetInterfaces().Contains(type) && c.IsAbstract == false)
           .ToList();

        if (implementations.Any() == false)
            throw new ArgumentException($"No implementing classes found for Injectable interface. {type.FullName}.");

        if (implementations.Count > 1 && attribute.AllowManyImplementations == false)
            throw new ArgumentException($"There can be only one implementation for Injectable interface {type.FullName}. Found {implementations.Count}.");

        switch (attribute.Lifetime)
        {
            case Lifetime.Transient:
                foreach (var implementation in implementations) 
                    services.AddTransient(type, implementation);
                
                break;
            case Lifetime.Singleton:
                foreach (var implementation in implementations) 
                    services.AddSingleton(type, implementation);

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attribute.Lifetime), attribute.Lifetime, null);
        }
    }
}
