## Description
Contexts are designed to separate the visibility scopes of containers. Each context has a bound container. Dependencies in these containers are unaware of dependencies in containers of other contexts.

## Context Types
Each context is represented under the common **IContext** interface.
- **MonoContext** - a context bound to the Unity lifecycle. Created by inheriting **MonoContext**.

**MonoContext** Example -
```csharp

public class CustomContext : MonoContext
{ 
    protected override void OnInitialize() 
    { 
        // The context will not be destroyed when the scene changes 
        DontDestroyOnLoad(gameObject) 
    }
}
```

- **Context** - a standard context whose lifecycle is completely controlled by the user.

**Context** Example -

```csharp
public class ContextStorage : MonoBehaviour
{ 
    public void Start() 
    { 
        var context = new Context() 
    } 
}
```
The internal structure of contexts can be seen by looking at the XML documentation within the code.

## Initializing Contexts
Special classes, **Installer-s**, are used to install dependencies.

As with contexts, there are two types of **Installer-s**, represented under the common **IInstaller** interface.

- MonoInstaller - **Installer**, represented as a component. Needed for installing dependencies in **MonoContext**, but can also be passed when creating **Context**

- **Installer** - The standard **Installer**; cannot be used to install dependencies within **MonoContext**, but can be used when creating **Context**

## Other
- For information on the parameters and internal structure of **Context** and **Installers**, see the XML documentation in the code.