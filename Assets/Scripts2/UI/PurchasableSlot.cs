using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;

public class PurchasableSlot : MonoBehaviour{
    int index;
    [HideInInspector] public GameObject itemOrHex;
    Item item;
    Hex hex;
    [HideInInspector] public int price;
    [Header("아이템")]
    [SerializeField] Image disabledIcon; // 슬롯의 비활성화 이미지
    [SerializeField] TMP_Text priceText; // 슬롯의 아이템 가격 텍스트
    [Header("버튼")]
    [SerializeField] public Button button; // button 컴포넌트. 인스펙터 창에서 연결
    // WorldObjectImage 넣는 홀더
    [SerializeField] GameObject holder;
    Shop shop;
    WorldObjectImage worldObjectImage;
    void Awake() {
        button.onClick.AddListener(Click);
        shop = GetComponentInParent<Shop>();
    }
    public void Init(GameObject itemOrHex, int index) {
        this.itemOrHex = itemOrHex;
        // 아이템인지 헥스인지 배정
        if (itemOrHex.GetComponent<Item>() != null) {
            item = itemOrHex.GetComponent<Item>();
            price = item.price;
        } else {
            hex = itemOrHex.GetComponent<Hex>();
            price = hex.price;
        }
        // 가격 텍스트 초기화
        priceText.text = price.ToString();
        // WorldObjectImage 초기화
        worldObjectImage = holder.GetComponent<WorldObjectImage>();
        StartCoroutine(worldObjectImage.Render(itemOrHex));
        // holder에 아이템넣기
        holder.GetComponent<ObjectHolder>().SetObject(itemOrHex);
        // disabledIcon 숨기기
        disabledIcon.enabled = false;
        this.index = index;
    }
    void Click() {
        StartCoroutine(OnClickSequence());
    }


    IEnumerator OnClickSequence() {
        yield return StartCoroutine(ClickEffect());
        yield return StartCoroutine(shop.TryPurchase(index));
    }

    // 클릭 시 일어나는 그래픽 이펙트
    IEnumerator ClickEffect(){

        yield return null;
    }
    // 구매 시 일어나는 그래픽 이펙트

    public IEnumerator OnPurchaseEffect() {
        GameObject target = worldObjectImage.gameObject;
        target.GetComponent<EnlargeOnHover>().enabled = false;
        float emphasisScale = 1.5f;
        float emphasisDuration = 0.3f;
        float disappearDuration = 0.5f;
        Vector3 initialScale = transform.localScale;
        Tween sequence = DOTween.Sequence()
            .Append(target.transform.DOScale(initialScale * emphasisScale, emphasisDuration))
            .AppendInterval(1f)
            .Append(target.transform.DOScale(Vector3.zero, disappearDuration + 0.1f))
            .Join(target.transform.DOMove(Constants.inventoryIconPosition, disappearDuration));
        yield return sequence.WaitForCompletion();
    }
    public IEnumerator DisableSlot() {
        disabledIcon.enabled = true;
        priceText.enabled = false;
        button.interactable = false;
        yield return null;
    }

    public void UpdateStatus(int newPrice) {
        priceText.text = newPrice.ToString();
        if (GameManager.instance.p.SearchItem(item)) {
            StartCoroutine(DisableSlot());
        }
    }
}