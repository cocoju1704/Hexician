using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHolder : MonoBehaviour {
    GameObject _obj;

    public GameObject obj {
        get {
            return _obj;
        }
    }

    public void SetObject(GameObject obj) {
        _obj = obj;
    }
}
