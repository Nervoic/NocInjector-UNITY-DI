using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NocInjector
{
    internal sealed class ResolverFactory
    {
        private readonly ConcurrentDictionary<Type, IResolver> _typeResolvers = new();
        
        private readonly HashSet<IResolver> _resolveMethods = new()
        {
            new DefaultResolver(),
            new ArrayResolver()
        };
        
        
        /// <summary>
        /// Selects the dependency resolve method for the type.
        /// </summary>
        /// <param name="dependencyType">The type of the dependency to be resolved</param>
        /// <returns></returns>
        /// <exception cref="ResolveUnsupportedTypeException"></exception>
        public IResolver SelectResolver(Type dependencyType)
        {
            if (_typeResolvers.TryGetValue(dependencyType, out var resolver))
                return resolver;

            resolver = _resolveMethods.FirstOrDefault(resolveMethod => resolveMethod.SupportType(dependencyType));
            
            if (resolver is null)
                throw new ResolveUnsupportedTypeException(dependencyType);
            
            _typeResolvers.TryAdd(dependencyType, resolver);
            return resolver;

        }
    }
}