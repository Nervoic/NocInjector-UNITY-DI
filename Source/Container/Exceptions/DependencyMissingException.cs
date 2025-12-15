using System;

namespace NocInjector
{
    public class DependencyMissingException : Exception
    {
        public DependencyMissingException(Type dependencyType, string dependencyTag) : base($"{dependencyType.Name} is not registered in the container with tag {dependencyTag}")
        {

        }

        public DependencyMissingException(Type dependencyType) : base($"{dependencyType.Name} is not registered in the container")
        {

        }
    }
}