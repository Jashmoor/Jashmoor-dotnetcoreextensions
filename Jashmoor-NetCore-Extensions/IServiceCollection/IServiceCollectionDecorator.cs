namespace Jashmoor_NetCore_Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;
    using System.Linq;

    public static class ServiceCollectionExtensions
    {
        public static void Decorate<TInterface, TConcrete>(this IServiceCollection services)
            where TInterface : class
            where TConcrete : class, TInterface
        {
            ServiceDescriptor wrappedDescriptor = services.FirstOrDefault(
              s => s.ServiceType == typeof(TInterface));

            _ = wrappedDescriptor ?? throw new InvalidOperationException($"{typeof(TInterface).Name} is not registered");

            ObjectFactory objectFactory = ActivatorUtilities.CreateFactory(
              typeof(TConcrete),
              new[] { typeof(TInterface) });

            services.Replace(ServiceDescriptor.Describe(
              typeof(TInterface),
              s => (TInterface)objectFactory(s, new[] { s.CreateInstance(wrappedDescriptor) }),
              wrappedDescriptor.Lifetime)
            );
        }

        private static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
                return descriptor.ImplementationInstance;

            if (descriptor.ImplementationFactory != null)
                return descriptor.ImplementationFactory(services);

            return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
        }
    }
}
