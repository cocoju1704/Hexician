using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class WorldObjectImage : MonoBehaviour {
    [SerializeField] Camera cam;
    RectTransform rect;
    RawImage image;
    RenderTexture rt;

    int width;
    int height;

    void Awake() {
        rect = GetComponent<RectTransform>();
        image = GetComponent<RawImage>();
    }
    // void Start() {
    //     cam = transform.GetChild(0).GetComponent<Camera>();
    // }

    public IEnumerator Render(GameObject obj)  {
        width = (int) rect.sizeDelta.x;
        height = (int) rect.sizeDelta.y;

        Vector3 newPos = obj.transform.position;
        newPos.z -= 1;

        cam.gameObject.SetActive(true);
        cam.transform.SetParent(null);
        cam.transform.position = newPos;

        rt = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
        cam.targetTexture = rt;
        image.texture = rt;
        yield return new WaitForEndOfFrame();

        yield return StartCoroutine(CameraOffCo());
    }

    public void Rerender() {
        cam.gameObject.SetActive(true);

        cam.targetTexture = rt;
        image.texture = rt;

        StartCoroutine(CameraOffCo());
    }

    IEnumerator CameraOffCo() {
        cam.transform.SetParent(transform);
        cam.gameObject.SetActive(false);
        yield return null;
    }
}
