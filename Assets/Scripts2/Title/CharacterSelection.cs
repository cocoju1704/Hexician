using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] bool canSelect;
    [SerializeField] Enums.CharacterType characterType;
    [SerializeField] GameObject dimObj;
    [SerializeField] Color normalColor;
    [SerializeField] Color highlightedColor;


    Image image;
    TitleMenu titleMenu;
    CharacterSelection[] selections;

    void Awake() {
        image = GetComponent<Image>();
    }

    void Start() {
        titleMenu = FindObjectOfType<TitleMenu>();
        selections = FindObjectsOfType<CharacterSelection>();
    }

    void OnEnable() {
        dimObj.SetActive(!canSelect);
    }

    void OnDisable() {
        titleMenu.SelectCharacter(Enums.CharacterType.None);
        TurnOffHighlight();
    }

    public void TurnOnHighlight() {
        image.color = highlightedColor;
    }

    public void TurnOffHighlight() {
        image.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(!canSelect) {
            return;
        }

        transform.DOScale(Vector3.one * 1.1f, 0.2f);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if(!canSelect) {
            return;
        }
        transform.DOScale(Vector3.one, 0.2f);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(!canSelect) {
            return;
        }

        titleMenu.SelectCharacter(characterType);
        foreach(CharacterSelection selection in selections) {
            if(selection.characterType == this.characterType) {
                selection.TurnOnHighlight();
            }
            else {
                selection.TurnOffHighlight();
            }
        }
    }
}