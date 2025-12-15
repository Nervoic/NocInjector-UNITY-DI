## Description
In order to inject dependencies or request them from the container, you need to register them.
For more information about **installing** dependencies, see the **Context and Installers** documentation.

## Registration
As described in **Context and installers.md **, you need **Installer** to register.
**Installer** must implement the **Install** method, and accept the **IContainerConstructor** parameter.

When accessing **IContainerConstructor**, we get access to registration.

Example of registration -

```csharp
public class PlayerInstaller : MonoInstaller
{
    public override void Install(IContainerConstructor constructor) 
    { 
        // Registration without specifying an abstraction
        constructor.Register<PlayerController>();
        
        // Registration as an interface implementation
        constructor.Register<IPlayerController, PlayerController>();
    }
}
```

## Floating registration
Immediately after using the **Register method** the container constructor gives you access to specify additional parameters for the dependency.

At the moment, the following methods are available for specifying additional parameters for the dependency -

- **WithTag** - allows you to set a tag for a dependency. The main thing is that the same dependency cannot be registered 2 times, and the same interface, regardless of which implementation is installed, cannot simply be registered 2 times. But if you specified an tag using this method during registration, then even dependencies of the same type, or the same interface, if they have different tags, will be independent of each other.


- **WithInstance** - allows you to manually set an instance for a dependency. It doesn't make sense to use this for Transient objects.


- **OnGameObject** - allows you to set the parent object of this component when registering components. If you do not install it, when injecting or manually requesting a dependency, the first component of this type found on the scene will be returned.


- **When** - takes bool as a parameter. If bool == false, it cancels the registration of this dependency.\


## Clarification
Registration, container assembly, validation, and injection take place in **Awake**. Use the **Start** method in Unity to ensure that all dependencies have been successfully implemented.