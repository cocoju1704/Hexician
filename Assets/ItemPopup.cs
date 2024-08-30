using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] GameObject itemObject;
    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text flavourText;
    // Start is called before the first frame update
    void Awake()
    {
    }
    public void Init(Item item) {
        name.text = item.name;
        description.text = item.description;
        flavourText.text = item.flavourText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
