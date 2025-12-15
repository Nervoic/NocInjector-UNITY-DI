using System;
using System.Collections.Generic;
using System.Linq;

namespace NocInjector
{
    internal class ResolveMethodFactory
    {
        private readonly Dictionary<Type, IResolveMethod> _cachedResolvers = new();
        
        private readonly HashSet<IResolveMethod> _resolveMethods = new()
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
        public IResolveMethod Select(Type dependencyType)
        {
            if (_cachedResolvers.TryGetValue(dependencyType, out var resolver))
                return resolver;

            resolver = _resolveMethods.FirstOrDefault(resolveMethod => resolveMethod.SupportResolveType(dependencyType));

            if (resolver is null)
                throw new ResolveUnsupportedTypeException(dependencyType);
            
            _cachedResolvers.Add(dependencyType, resolver);
            return resolver;

        }
    }
}