# MoDI documentation
MoDI is designed with one goal in mind – simple old school DI for Unity. You can try to use it outside of Unity, but current version targets to using with MonoBehaviours, mostly.

## Introduction
There are few entities in MoDI: manager, containers and components. That's all.
Manager controls containers. Containers controls components. Components stores bindings with some options.

`DIManager` can create new containers, return container by tag and rules the injections.

`DIContainer` is a main object to work with. If you need to bind some type or resolve object for type – use methods of it.

`DIComponent` is a inner helper class to store bind options and instance. Call DIComponent's builder methods to set custom id or pass arguments.

[MoDI API](#api) ⬇️

## Starter script
First of all you have to create *starter* script that will be runs before all scripts where you resolve objects and before all __DIMonoBehaviour__ scripts, where MoDI inject data into fields. It can be your own app entry point or MonoBehaviour script with custom execution order.

```csharp
using MoDI;
using UnityEngine;

/// <summary>
/// It's a starter script to bind all types and instances
/// </summary>
[DefaultExecutionOrder(-100)]
public class Starter : MonoBehaviour {

    void Start() {
        DI.Get().Bind<InnerClass>();
        ...
    }

}
```

## Binding
In example below you can see variants of binding. Component's builder can chain methods to set options.

```csharp
using MoDI;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Starter : MonoBehaviour {

    private InnerClass5 _inner5 = new InnerClass5("data1", 999);

    void Start() {
        DI.Get().Bind<InnerClass>();
        DI.Get().Bind<InnerClass2>().AsSingle();
        DI.Get().Bind<InnerClass3>().WithArguments("data");
        DI.Get().Bind<InnerClass4>().WithArguments("data").WithId("myId");
        DI.Get().Bind<InnerClass5>().WithId("data1").FromInstance(_inner5);  
        DI.Manager.ApplyInject();      
    }

}
```

Or you can bind initialized instance, with options too.

```csharp
using MoDI;
using UnityEngine;

public interface IServiceOne { ... }

public interface IServiceTwo { ... }

public class ServiceTest : MonoBehaviour, IServiceOne, IServiceTwo {
...
}

[DefaultExecutionOrder(-100)]
public class Starter : MonoBehaviour {

    [SerializeField]
    private ServiceTest _service;

    void Start() {
        DI.Get().BindInstance(_service).As<IServiceOne>().As<IServiceTwo>();     
    }

}
```

## Resolving
After starter bind process, you can resolve objects anywhere you need. Like for binding, you can resolve object with custom id.

```csharp
using MoDI;
using UnityEngine;

public class TestClass : DIMonoBehaviour {

    void Start() {
        InnerClass1 _inner1 = DI.Get().Resolve<InnerClass1>();
        InnerClass2 _inner2 = DI.Get().Resolve<InnerClass2>("myId");
    }

}
```

## Injectiton
You can tell MoDI to inject objects into all fields in class and, if necessary, ignore some of them.

```csharp
using MoDI;
using UnityEngine;

[Inject]
public class TestClass : DIMonoBehaviour {

    private InnerClass1 _inner1 = null;

    private InnerClass2 _inner2 = null;

    [IgnoreInject]
    private InnerClass3 _inner3 = null;

    private InnerClass4 _inner4 = null;

    void Start() {
        ...
    }

}
```

Or you can set `[Inject]` attribute to selected fields. Also, you can pass param in attribute contructor to inject data into field by id.

```csharp
using MoDI;
using UnityEngine;

public class TestClass : DIMonoBehaviour {

    [Inject]
    private InnerClass1 _inner1 = null;

    [Inject("myId")]
    private InnerClass2 _inner2 = null;

    void Start() {
        ...
    }

}
```

To apply injections, you have to call special method `DI.Manager.ApplyInject()`. Manager will find all `DIMonoBehaviour` helper classes on scene and inject objects into fields. 

> NB: `DIMonoBehaviour` helper class uses default container.

## Manager
Every time you call `DI.Get()`, manager returns default container. But you can add more containers and use them passing container tag to `Get()` method, e.g. for classification your bindings.

```csharp
using MoDI;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Starter : MonoBehaviour {

    void Start() {
        DI.Manager.Add("my_container").Bind<InnerClass>();
        ...
        InnerClass inner = DI.Get("my_container").Resolve<InnerClass>();    
    }

}
```

## API

### DIManager

```csharp
/// <summary>
/// Initialize DI manager
/// Create default container
/// </summary>
public DIManager Initialize();

/// <summary>
/// Add new container to manager
/// </summary>
/// <param name="tag">Container tag</param>
/// <param name="capacity">Container capacity</param>
/// <returns>New container</returns>
public DIContainer Add(string tag, int capacity = CONTAINER_CAPACITY);

/// <summary>
/// Get container for a work by tag
/// </summary>
/// <param name="tag">Container tag</param>
/// <returns>Container for a work</returns>
public DIContainer Get(string tag);

/// <summary>
/// Inject objects to fields with attributes
/// </summary>
/// <param name="sender">Instance to get fields</param>
/// <param name="tag">Container tag</param>
public void Inject(object sender, string tag = DI.CONTAINER_TAG);

/// <summary>
/// Inject for all DIMonoBehaviours on scene
/// </summary>
public void ApplyInject();

/// <summary>
/// ACHTUNG!
/// Clear all binds and remove all containers
/// </summary>
public void Clear();
```

### DIContainer

```csharp
/// <summary>
/// Bind instance 
/// Type will be get from itself
/// </summary>
/// <param name="instance"></param>
public IDIComponentBuilder BindInstance(object instance);

/// <summary>
/// Bind type
/// Instance is NULL by default
/// </summary>
/// <param name="type">Type to bind</param>
/// <param name="instance">Instance to bind</param>
public IDIComponentBuilder Bind(Type type, object instance = null);

/// <summary>
/// Bind type by generic
/// </summary>
/// <typeparam name="T">Type to bind</typeparam>
public IDIComponentBuilder Bind<T>();

/// <summary>
/// Resolve object for field
/// Check id if need get by custom bind id
/// </summary>
/// <param name="field">FieldInfo from DIMonoBehaviour</param>
public object Resolve(FieldInfo field);

/// <summary>
/// Resolve object for type
/// Id is optional
/// </summary>
/// <param name="type">Type to resolve</param>
/// <param name="id">Custom bind id</param>
public object Resolve(Type type, string id = null);

/// <summary>
/// Resolve object for type by generic
/// Id is optional
/// </summary>
/// <typeparam name="T">Type to resolve</typeparam>
/// <param name="id">Custom bind id</param>
public T Resolve<T>(string id = null);

/// <summary>
/// Clear container
/// </summary>
public void Clear();
```

### DIComponent

```csharp
/// <summary>
/// Check type in component's types list
/// </summary>
/// <param name="type">Type to check</param>
/// <returns>Contains or not</returns>
public bool ContainsType(Type type);

/// <summary>
/// Bind as type
/// </summary>
/// <param name="type">Type to bind</param>
public IDIComponentBuilder As(Type type);

/// <summary>
/// Bind as type by generic
/// </summary>
/// <typeparam name="T">Type to bind</typeparam>
public IDIComponentBuilder As<T>();

/// <summary>
/// Bind type from selected instance
/// </summary>
/// <param name="instance">Instance to bind</param>
public IDIComponentBuilder FromInstance(object instance);

/// <summary>
/// Use bind as single
/// All other binds with this type will be ignored
/// </summary>
public IDIComponentBuilder AsSingle();

/// <summary>
/// Pass arguments to constructor
/// </summary>
/// <param name="list">Arguments</param>
public IDIComponentBuilder WithArguments(params object[] list);

/// <summary>
/// Define custom id for bind
/// </summary>
/// <param name="id">Custom bind id</param>
public IDIComponentBuilder WithId(string id);
```

### DI

```csharp
/// <summary>
/// Initialize DIManager and return link
/// </summary>
public static DIManager Manager;

/// <summary>
/// Get container for bind or resolve
/// </summary>
/// <param name="tag">Container tag</param>
/// <returns>DIContainer for a work</returns>
public static DIContainer Get(string tag = CONTAINER_TAG);
```