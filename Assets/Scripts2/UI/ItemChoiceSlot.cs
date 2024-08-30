using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;

public class ItemChoiceSlot : MonoBehaviour{
    int index;
    [HideInInspector] public GameObject itemOrHex;
    Item item;
    Hex hex;
    [Header("아이템")]
    [Header("버튼")]
    [SerializeField] Button button; // button 컴포넌트. 인스펙터 창에서 연결
    [SerializeField] TextMeshProUGUI description; // 텍스트 컴포넌트. 인스펙터 창에서 연결
    [SerializeField] ItemChoiceUI itemChoiceUI;
    WorldObjectImage worldObjectImage;
    void Awake() {
        button.onClick.AddListener(Click);
        itemChoiceUI = GetComponentInParent<ItemChoiceUI>();
    }
    public void Init(GameObject itemOrHex, int index) {
        this.itemOrHex = itemOrHex;
        // 아이템인지 헥스인지 배정
        if (itemOrHex.GetComponent<Item>() != null) {
            item = itemOrHex.GetComponent<Item>();
        } else {
            hex = itemOrHex.GetComponent<Hex>();
        }
        // 텍스트 초기화
        description.text = item.description;
        // WorldObjectImage 초기화
        worldObjectImage = itemOrHex.GetComponent<WorldObjectImage>();
        worldObjectImage.transform.SetParent(transform);
        worldObjectImage.transform.localPosition = new Vector3Int(0, 50, 0);
        this.index = index;
    }
    void Click() {
        StartCoroutine(itemChoiceUI.ChooseEffect(index));
    }

    public IEnumerator ChosenGraphicEffect() {
        GameObject target = worldObjectImage.gameObject;
        target.GetComponent<EnlargeOnHover>().enabled = false;
        description.enabled = false;

        Vector3 initialScale = transform.localScale;
        float emphasisScale = 1.5f;
        float emphasisDuration = 0.3f;
        float disappearDuration = 0.5f;
        Tween sequence = DOTween.Sequence()
            .Append(target.transform.DOScale(initialScale * emphasisScale, emphasisDuration))
            .AppendInterval(1f)
            .Append(target.transform.DOScale(Vector3.zero, disappearDuration))
            .Join(target.transform.DOMove(Constants.inventoryIconPosition, disappearDuration));
        yield return sequence.WaitForCompletion();
    }
    public IEnumerator NotChosenGraphicEffect() {
        GameObject target = worldObjectImage.gameObject;
        target.GetComponent<EnlargeOnHover>().enabled = false;
        description.enabled = false;

        target.transform.DOScale(Vector3.zero, 0.1f);
        Destroy(gameObject, 0.5f);
        yield return null;
    }
}