using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour {
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI hpTMP;
    [SerializeField] TextMeshProUGUI armorTMP;

    protected Unit unit;

    public virtual void Init(Unit unit) {
        this.unit = unit;
        
        unit.OnChangeStatus.AddListener(UpdateUI);
        UpdateUI();
    }

    protected virtual void UpdateUI() {
        slider.maxValue = unit.maxHp;
        slider.value = unit.currentHp;
        
        hpTMP.text = unit.currentHp.ToString() + "/" + unit.maxHp.ToString();
        armorTMP.text = unit.armor.ToString();
    }

    void OnDestroy() {
        unit.OnChangeStatus.RemoveListener(UpdateUI);
    }
}
