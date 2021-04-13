<p align="center">
    <img src="Documentation~/MoDI.png?raw=true" alt="MoDI" width="580px" height="259px"/>
</p>

# MoDI
MoDI is a lightweight IoC container developed specifically to use with Unity. It's inspired by Zenject, but not so complex. Just one more bicycle for binding types, instances, resolving them and injection into fields 🙃

## Installation ![GitHub release (latest by date)](https://img.shields.io/github/package-json/v/mopsicus/modi)
Get it from [releases page](https://github.com/mopsicus/modi/releases) or add the line below to `Packages/manifest.json` and this module will be installed directly from git url:
```
"com.mopsicus.modi": "https://github.com/mopsicus/modi.git",
```

## How to use
See all API methods and examples in [Documentation](Documentation~/MoDI.md).

## Quick start
To start use MoDI you shouldn't call any initialize methods. When you first call `DI.Get()` then MoDI already initialized and ready to work. Just add `using MoDI;` to your scripts and go ahead.

Call `DI.Get()` to get default container. Pass container tag into `Get("my_container")` method to return custom container.

Call `DI.Get().Bind<YourType>()` to make bind in container. Your can combine builder methods in chain: `DI.Get().Bind<YourType>().WithId("id1").AsSingle()`.

To resolve object from container call `DI.Get().Resolve<YourType>()`.

To mark field for injection set `[Inject]` attribute to it.

Simplest example:

```csharp
using MoDI;
using UnityEngine;

public class QuickStart : MonoBehaviour {

    public void Start() {
        DI.Get().Bind<Hello>().WithArguments("Hi, I'm MoDI!");
        Hello hello = DI.Get().Resolve<Hello>();
    }
    
}

public class Hello {

    public Hello(string data) {
        Debug.Log(data);
    }

}
```

After run this script you can see __`Hi, I'm MoDI!`__ message in console.

## Demo
You can import demo scene and scripts via UPM or find them in `Samples~/Demo` directory.
