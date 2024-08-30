using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceSystem2 : Singleton<ResourceSystem2> {
    public Dictionary<Enums.CharacterType, PlayerData> playerDataDictionary;
    public List<SOHexData> hexDataList;
    public List<Item> itemList;

    public int maxItemCount;
    public int maxHexCount;

    void Start() {
        LoadPlayerData();
        LoadAllHex();
        LoadAllItem();
    }

    void LoadPlayerData() {
        playerDataDictionary = new Dictionary<Enums.CharacterType, PlayerData>();

        string fileName = "PlayerData";

        // foreach(Enums.CharacterType type in System.Enum.GetValues(typeof(Enums.CharacterType))) {
        //     if(type == Enums.CharacterType.None) {
        //         return;
        //     }
            
        //     string filePath = Path.Combine(Application.dataPath, fileName + type.ToString());
        //     string jsonFormat = File.ReadAllText(filePath);
        //     PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonFormat);
            
        //     playerDataDictionary.Add(type, data);
        // }
    }

    void LoadAllHex() {
        hexDataList = Resources.LoadAll<SOHexData>("HexData").OrderBy((data) => data.id).ToList();
    }

    void LoadAllItem() {
        for (int i = 1; i <= maxItemCount; i++) {
            Item item = Resources.Load("Prefabs/Items/item_" + i).GetComponent<Item>();
            itemList.Add(item);
        }
    }

    public PlayerData GetPlayerData(Enums.CharacterType type) {
        if(playerDataDictionary.ContainsKey(type)) {
            return playerDataDictionary[type];
        }
        else {
            return null;
        }        
    }
    public Hex MakeHex(int id) {
        return hexDataList[id].CreateHex();
    }

    public Item MakeItem(int no) {
        return Instantiate(itemList[no]).GetComponent<Item>();
    }
    
    public Item MakeItem(int no, Transform parent) {
        return Instantiate(itemList[no], parent).GetComponent<Item>();
    }
    public List<Item> MakeItem(List<int> ids) {
        List<Item> items = new List<Item>();
        foreach(int id in ids) {
            items.Add(MakeItem(id));
        }
        return items;
    }
    public List<Hex> MakeHex(List<int> ids) {
        List<Hex> hexes = new List<Hex>();
        foreach(int id in ids) {
            hexes.Add(MakeHex(id));
        }
        return hexes;
    }
}