using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit, ISavable {
    static Player _instance;
    public static Player instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<Player>();
            }
            return _instance;
        }
    }

    public int gold;
    public Vector3Int coordinate;
    public List<int> hexIds;
    public List<Item> items;
    public List<int> itemIds;
    public Dictionary<Vector3Int, bool> activatedTiles;
    public Dictionary<string, int> discounts;
    
    public UnityEvent<Item, int> OnAddItem;
    [SerializeField] Transform itemPoint;

    void Awake() {
        OnAddItem = new UnityEvent<Item, int>();
        ActivateDebugMode();
    }

    public void SetPlayer(PlayerData playerData) {
        this.maxHp = playerData.maxHp;
        this.currentHp = playerData.currentHp;
        this.gold = playerData.gold;
        
        this.coordinate = playerData.coordinate;
        
        this.hexIds = new List<int>(playerData.hexIds);
        this.discounts = new Dictionary<string, int>();

        foreach(int id in playerData.itemIds) {
            Item item = ResourceSystem.instance.MakeItem(id);
            AddItem(item);  
        }

        this.activatedTiles =  new Dictionary<Vector3Int, bool>(playerData.activatedTiles);
    }

    public void Save(GameData gameData) {
        PlayerData playerData = gameData.playerData;

        playerData.maxHp = this.maxHp;
        playerData.currentHp = this.currentHp;
        playerData.gold = this.gold;
        
        playerData.coordinate = this.coordinate;
        
        playerData.hexIds = new List<int>(this.hexIds);

        playerData.itemIds.Clear();
        foreach(Item item in this.items) {
            playerData.itemIds.Add(item.no);
        }

        playerData.activatedTiles = new Dictionary<Vector3Int, bool>(this.activatedTiles);

        Debug.Log("Saved Player Data");
    }

    public void Load(GameData gameData) {
        SetPlayer(gameData.playerData);
    }

    public void AddItem(Item item) {
        ItemEvents.instance.OnItemObtain.Invoke(item);
        items.Add(item);
        item.transform.position = itemPoint.position + new Vector3(100, 0, 0) * (items.Count - 1);
        item.transform.SetParent(transform);
        OnAddItem.Invoke(item, items.Count - 1);
    }
    public void RemoveItem(Item item) {
        items.Remove(item);
        Destroy(item.gameObject);
    }
    public void RemoveRecentItem() {
        if (items.Count > 0) {
            RemoveItem(items.Last());
        }
    }
    public bool SearchItem(Item item) {
        foreach (Item i in items) {
            if (i.no == item.no) {
                return true;
            }
        }
        return false;
    }
    public List<int> GetHexes() {
        return hexIds;
    }
    public void ActivateDebugMode() {
        gold = 100;
    }
}
