using MoDI;
using UnityEngine;

/// <summary>
/// Demo to show inject fields for whole class
/// IgnoreInject attribute set which field doesn't need to inject
/// </summary>
[Inject]
public class InjectClassTest : DIMonoBehaviour {

    private InnerClass1 _inner1 = null;

    private InnerClass2 _inner2 = null;

    [IgnoreInject]
    private InnerClass3 _inner3 = null;

    void Start() {
        Debug.Log("---- start InjectClassTest ----");
        if (_inner1 != null) {
            _inner1.ShowMessage();
        } else {
            Debug.LogWarning("_inner1 is NULL");
        }
        if (_inner2 != null) {
            _inner2.ShowMessage();
        } else {
            Debug.LogWarning("_inner2 is NULL");
        }
        if (_inner3 != null) {
            _inner3.ShowMessage();
        } else {
            Debug.LogWarning("_inner3 is NULL");
        }
        Debug.Log("---- end InjectClassTest ----\n");
    }

}
