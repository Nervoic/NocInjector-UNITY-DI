## Description
Dependency injection is used to automatically inject dependencies into classes and components that require them.

## How injection works
Generally, injection occurs through methods or constructors. NocInjector does not support injection into fields and properties. To inject dependencies into a method, use the **[Inject]** attribute.

Example of using the **[Inject]** attribute -

```csharp

public class PlayerController : MonoBehaviour
{ 
    private PlayerSpeedInfo _speedInfo; 

    [Inject]
    private void Construct(PlayerSpeedInfo speedInfo)
    
    { 
        _speedInfo = speedInfo 
    }
}
```

Injection into the constructor occurs in the same way, only using the **[Inject]** attribute.

```csharp
public class PlayerController : MonoBehaviour
{ 
    private readonly PlayerSpeedInfo _speedInfo;

    [Inject]
    public PlayerController(PlayerSpeedInfo speedInfo)
    
    { 
        _speedInfo = speedInfo 
    }
}
```

## Injection Specifics
Injection occurs only from the same container in which the dependency was registered.
If this container does not have the dependency to be injected, the container looks for this dependency in the parent container (if a parent context was assigned to this container when the context was created).

## Component Injection -
This only occurs if the component is registered in a context, and injection, as mentioned earlier, occurs from the container of the same context in which it was registered, or its parent container.

If a component is registered as Singleton, injection occurs only into that component.

If a component is registered as Transient, then when this component is requested, a new instance of the component's GameObject will be created, and the dependencies of all its components will be injected into this newly created GameObject.

## Standard Injection -

If you register a dependency in a container that is not a component, you can use the **[Inject]** attribute in the dependency's constructor. Method injection not work in this case.

## Other
We also recommend that you read the registration documentation, which describes the registration process in detail.