using System;

namespace NocInjector
{
    public class RepeatedDependencyException : Exception
    {
        public RepeatedDependencyException(Type dependencyType, string dependencyTag) : base($"{dependencyType.Name} already registered on tag {dependencyTag}") { }
    }
}