using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorNo : MonoBehaviour {
    [SerializeField] List<Sprite> floorSprites;
    [SerializeField] Image image;
    Stage stage;

    void Start() {
        stage = FindObjectOfType<Stage>();

        image.sprite = floorSprites[stage.floorNo];
    }
}