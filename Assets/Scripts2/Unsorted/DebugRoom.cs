using UnityEngine;

public class DebugRoom : MonoBehaviour
{
    [SerializeField] GameObject DebugObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            Instantiate(DebugObject, canvas.transform);
        }
    }
}
