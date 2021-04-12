using MoDI;
using UnityEngine;

/// <summary>
/// It's a starter script to bind all types and instances
/// </summary>
[DefaultExecutionOrder(-100)]
public class Starter : MonoBehaviour {

    private InnerClass4 _inner4 = null;

    void Start() {
        _inner4 = new InnerClass4("from instance");
        DI.Get().Bind<InnerClass1>();
        DI.Get().Bind<InnerClass2>();
        DI.Get().Bind<InnerClass3>();
        DI.Get().Bind<InnerClass4>().WithArguments("class param");
        DI.Get().Bind<InnerClass4>().WithArguments("for my id 1").WithId("myId");
        DI.Get().Bind<InnerClass4>().WithArguments("for my id 2").WithId("myId2");
        DI.Get().Bind<InnerClass4>().WithId("myId3").FromInstance(_inner4);
        DI.Get().Bind<InnerClass5>().WithArguments(100).AsSingle();
        DI.Get().Bind<InnerClass5>().WithArguments(999);
        DI.Get().Bind<InjectClassTest>();
        DI.Get().Bind<InjectFieldsTest>();
        DI.Get().Bind<InjectParamsTest>();
        DI.Get().Bind<InjectIdTest>();
        DI.Get().Bind<InjectSingleTest>();
        DI.Get().Bind<InjectInstanceTest>();
        DI.Get().Bind<InjectCodeTest>();
        DI.Manager.ApplyInject();
    }

}
