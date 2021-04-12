using MoDI;
using UnityEngine;

/// <summary>
/// Demo to show inject object from instance
/// </summary>

public class InjectInstanceTest : DIMonoBehaviour {

    [Inject("myId3")]
    private InnerClass4 _inner4 = null;

    void Start() {
        Debug.Log("---- start InjectInstanceTest ----");
        if (_inner4 != null) {
            _inner4.ShowMessage();
        } else {
            Debug.LogWarning("_inner4 is NULL");
        }
        Debug.Log("---- end InjectInstanceTest ----\n");
    }

}
