using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Wigor.Sample.Infrastructure.Repository
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection serviceCollection, Type typeofRepository)
        {
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var types = Assembly.GetAssembly(typeofRepository).GetTypes()
                .Where(w => w.Name.EndsWith("Repository") && w.IsClass && !w.IsAbstract);

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces()
                    .FirstOrDefault(w => w.Name == $"I{type.Name}");

                if (interfaceType == null)
                    throw new NotImplementedException($"Not found Interface:{nameof(type)}");

                serviceCollection.AddSingleton(interfaceType, type);
            }

            return serviceCollection;
        }
    }
}
