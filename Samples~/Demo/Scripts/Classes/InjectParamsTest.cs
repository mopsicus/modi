using MoDI;
using UnityEngine;

/// <summary>
/// Demo to show inject selected fields in class where injected object has arguments
/// </summary>
public class InjectParamsTest : DIMonoBehaviour {

    [Inject]
    private InnerClass4 _inner4 = null;

    void Start() {
        Debug.Log("---- start InjectParamsTest ----");
        if (_inner4 != null) {
            _inner4.ShowMessage();
        } else {
            Debug.LogWarning("_inner4 is NULL");
        }
        Debug.Log("---- end InjectParamsTest ----\n");
    }

}
