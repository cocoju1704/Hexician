using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem> {
    public Dictionary<Enums.CharacterType, PlayerData> playerDataDictionary;
    public List<SOHexData> hexDataList;
    public List<Item> allItemList;

    [HideInInspector] int maxItemCount = 10;
    [HideInInspector] int maxHexCount;

    void Start() {
        LoadPlayerData();
        LoadAllHex();
        LoadAllItem();
    }

    void LoadPlayerData() {
        playerDataDictionary = new Dictionary<Enums.CharacterType, PlayerData>();

        string fileName = "PlayerData";

        foreach(Enums.CharacterType type in System.Enum.GetValues(typeof(Enums.CharacterType))) {
            if(type == Enums.CharacterType.None) {
                return;
            }
            
            string filePath = Path.Combine(Application.dataPath, fileName + type.ToString());
            string jsonFormat = File.ReadAllText(filePath);
            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonFormat);
            
            playerDataDictionary.Add(type, data);
        }
    }

    void LoadAllHex() {
        hexDataList = new List<SOHexData>();
        hexDataList = Resources.LoadAll<SOHexData>("HexData").OrderBy((data) => data.id).ToList();
    }

    void LoadAllItem() {
        allItemList.Add(null);
        for (int i = 1; i <= maxItemCount; i++) {
            GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Items/Item " + i);
            if (itemPrefab != null) {
                Item item = itemPrefab.GetComponent<Item>();
                item.no = i;
                allItemList.Add(item);
            }
        }
    }

    public PlayerData GetPlayerData(Enums.CharacterType type) {
        if(playerDataDictionary.ContainsKey(type)) {
            return playerDataDictionary[type];
        }
        else {
            return new PlayerData();
        }        
    }





    public Item MakeItem(int no) {
        Debug.Log("Made Item" + no);
        Debug.Log(allItemList.Count);
        return Instantiate(allItemList[no]).GetComponent<Item>();
    }
    public Item MakeItem(int no, Transform parent) {
        return Instantiate(allItemList[no], parent).GetComponent<Item>();
    }
    public Item MakeItem(int no, Vector3 position) {
        return Instantiate(allItemList[no], position, Quaternion.identity).GetComponent<Item>();
    }
    public List<Item> MakeItem(List<int> ids) {
        List<Item> items = new List<Item>();
        Vector3Int startPos = new Vector3Int(-1000, -1000, 0);
        for (int i = 0; i < ids.Count; i++) {
            items.Add(MakeItem(ids[i], startPos + new Vector3(100, 0, 0) * i));
        }
        return items;
    }
    public Hex MakeHex(int id) {
        return hexDataList[id].CreateHex();
    }
    public List<Hex> MakeHex(List<int> ids) {
        List<Hex> hexes = new List<Hex>();
        foreach(int id in ids) {
            hexes.Add(MakeHex(id));
        }
        return hexes;
    }
}