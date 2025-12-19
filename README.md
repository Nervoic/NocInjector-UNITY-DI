<img width="4200" height="1500" alt="NOCINJECTOR_Banner" src="https://github.com/user-attachments/assets/cd82e2bc-40e8-4a02-9dca-7313176a561f" />

<br>

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2022+-black.svg?style=for-the-badge&logo=unity)
![License](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)
![Version](https://img.shields.io/badge/version-26.0.0.2-green.svg?style=for-the-badge)
![Platforms](https://img.shields.io/badge/platforms-PC%20%7C%20WebGL%20%7C%20Android-lightgrey?style=for-the-badge)

<br>

> **Unity 2022+ | Stable on IL2CPP, PC, WebGL, Android builds**

</div>

---

## 🚀 What is NocInjector?

**NocInjector** is a modern, high-performance, lightweight Dependency Injection library for Unity. Designed to combine simplicity, flexibility, and exceptional performance while maintaining functionality across virtually any device.

---

## ✨ Advantages

### 🏎️ **Performance & Efficiency**
- **High runtime performance**
- **Zero Allocation** in hot paths
- **Low memory consumption**
- **Mobile-optimized** for battery and performance

### 🛠️ **Developer Experience**
- **Simple and readable syntax**
- **Fast learning curve**
- **Detailed documentation**
- **Test-friendly** architecture
- **Conditional compilation** support

### 🔧 **Technical Features**
- **Flexible dependency management**
- **Method and constructor injection**
- **Dependency parameters** during registration
- **Dependency lifetimes** (Singleton, Transient, etc.)
- **Automatically dispose** resources

### 🏗️ **Architecture & Compatibility**
- **Contexts scope system** for modular design
- **Highly extensible** plugin system
- **AOT Compatibility** (full IL2CPP support)
- **Thread Safety** for concurrent operations

---

## 📦 Usage

To use the library, you need to:

1. **Download** the latest released version
2. **Install** the `.unitypackage` in your Unity project
3. **Check** the quick start guide below

---

## 🚀 Quick Start
### 1. Create custom **Context**
```csharp
using NocInjector;
using UnityEngine;
    
public class CustomContext : MonoContext 
{
    // It may be empty
}
```
### 2. Create **Installer**
```csharp
using NocInjector;
using UnityEngine;
    
public class CustomInstaller : MonoInstaller
{
    public override void Install(IContainerConstructor constructor) 
    {
        constructor.Register<MyClass>(DependencyLifetime.Singleton);
    }
}
```
Drag this **Installer** into the **Installers field** on your context
### 3. Inject dependencies
```csharp
using NocInjector;
using UnityEngine;
    
public class CustomBehaviour : MonoBehaviour
{
    private MyClass _classInstance;
    
    public void Construct(MyClass classInstance) 
    {
        _classInstance = classInstance;
    }
}
```
The **GameObject** containing this script should be dragged into the **Injection objects field** on your **Context**


### **For detailed information about the library and usage instructions, see the Documentation folder.**
## 💬 Developer Feedback -
- Discord `nervoic`
