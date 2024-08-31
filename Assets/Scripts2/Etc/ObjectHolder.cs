using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Focus.cs에서 WorldObjectImage에서 역으로 GameObject를 찾아야 하는 경우가 있어서, GameObject를 가지고 있는 ObjectHolder를 만듦
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
