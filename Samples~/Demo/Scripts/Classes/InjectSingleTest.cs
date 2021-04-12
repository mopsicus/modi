using MoDI;
using UnityEngine;

/// <summary>
/// Demo to show inject object binded as single
/// All other binds will be ignored
/// </summary>
[Inject]
public class InjectSingleTest : DIMonoBehaviour {

    private InnerClass5 _inner5 = null;

    private InnerClass5 _inner52 = null;

    void Start() {
        Debug.Log("---- start InjectSingleTest ----");
        if (_inner5 != null) {
            _inner5.ShowMessage();
        } else {
            Debug.LogWarning("_inner5 is NULL");
        }
        if (_inner52 != null) {
            _inner52.ShowMessage();
        } else {
            Debug.LogWarning("_inner52 is NULL");
        }
        Debug.Log("---- end InjectSingleTest ----\n");
    }

}
