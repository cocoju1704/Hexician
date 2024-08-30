using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    [SerializeField] GameObject slotObj;
    List<WorldObjectImage> images;
    GridLayoutGroup grid;

    bool isOpen;

    void Awake() {
        grid = GetComponent<GridLayoutGroup>();
        images = new List<WorldObjectImage>();

        transform.localScale = Vector3.zero;
        isOpen = false;
    }

    void Start() {
        Init(FindObjectOfType<Player>());
    }

    public void Toggle() {
        if(isOpen) {
            Close();
        }
        else {
            Open();
        }
    }

    void Open() {
        isOpen = true;
        transform.DOScale(Vector3.one, 0.1f);
    }

    void Close() {
        isOpen = false;
        transform.DOScale(Vector3.zero, 0.1f);
    }

    void Init(Player p) {
        p.OnAddItem.AddListener(AddItem);

        grid.constraint = GridLayoutGroup.Constraint.Flexible;
        foreach(WorldObjectImage image in images) {
            image.gameObject.SetActive(false);
        }
    }

    void AddItem(Item item, int idx) {
        if(idx == 7) {
            grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.constraintCount = 8;
        }

        while(idx >= images.Count) {
            MakeNewSlot();
        }

        WorldObjectImage image = images[idx];
        StartCoroutine(image.Render(item.gameObject));
    }

    void MakeNewSlot() {
        WorldObjectImage image = Instantiate(slotObj).GetComponent<WorldObjectImage>();
        image.transform.SetParent(transform);
        image.transform.localScale = Vector3.one;

        images.Add(image);
    }
}
