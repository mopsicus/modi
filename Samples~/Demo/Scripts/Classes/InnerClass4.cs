using UnityEngine;

public class InnerClass4 {

    private string _data = null;

    public InnerClass4(string data) {
        _data = data;
    }

    public void ShowMessage() {
        Debug.LogFormat("InnerClass4 message: {0}", _data);
    }

}

