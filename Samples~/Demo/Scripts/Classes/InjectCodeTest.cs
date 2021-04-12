using System.Runtime.InteropServices.ComTypes;
using MoDI;
using UnityEngine;

/// <summary>
/// Demo to show resolve object by code
/// </summary>

public class InjectCodeTest : DIMonoBehaviour {

    private InnerClass5 _inner5 = null;

    void Start() {
        _inner5 = DI.Get().Resolve<InnerClass5>();
        Debug.Log("---- start InjectCodeTest ----");
        if (_inner5 != null) {
            _inner5.ShowMessage();
        } else {
            Debug.LogWarning("_inner5 is NULL");
        }
        Debug.Log("---- end InjectCodeTest ----\n");
    }

}
