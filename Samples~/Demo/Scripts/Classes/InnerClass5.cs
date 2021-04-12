using UnityEngine;

public class InnerClass5 {

    private int _data = -1;

    public InnerClass5(int data) {
        _data = data;
    }

    public void ShowMessage() {
        Debug.LogFormat("InnerClass5 data: {0}", _data);
    }

}

