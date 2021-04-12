using MoDI;
using UnityEngine;

/// <summary>
/// Demo to show inject selected fields in class with custom id
/// </summary>
public class InjectIdTest : DIMonoBehaviour {

    [Inject("myId")]
    private InnerClass4 _inner4MyId = null;

    [Inject("myId2")]
    private InnerClass4 _inner4MyId2 = null;

    [Inject]
    private InnerClass4 _inner4Without = null;

    void Start() {
        Debug.Log("---- start InjectIdTest ----");
        if (_inner4MyId != null) {
            _inner4MyId.ShowMessage();
        } else {
            Debug.LogWarning("_inner4MyId is NULL");
        }
        if (_inner4MyId2 != null) {
            _inner4MyId2.ShowMessage();
        } else {
            Debug.LogWarning("_inner4MyId2 is NULL");
        }
        if (_inner4Without != null) {
            _inner4Without.ShowMessage();
        } else {
            Debug.LogWarning("_inner4Without is NULL");
        }
        Debug.Log("---- end InjectIdTest ----\n");
    }

}
