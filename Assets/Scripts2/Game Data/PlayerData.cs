using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerData {
    public int maxHp;
    public int currentHp;
    public int gold;
    public Vector3Int coordinate;
    public List<int> hexIds;
    public List<int> itemIds;
    public Dictionary<Vector3Int, bool> activatedTiles;

    public List<ItemData> itemDataList;

    public PlayerData() {
        maxHp = 50;
        currentHp = 50;
        gold = 50;

        coordinate = Vector3Int.zero;
        
        hexIds = new List<int>();
        itemIds = new List<int>();

        activatedTiles = new Dictionary<Vector3Int, bool>();
        itemDataList = new List<ItemData>();

        Vector3Int currCoordinate;
        for(int radius = 1; radius <= 2; radius++) {
            currCoordinate = Vector3Int.zero + Utils.Vectors[(int) Enums.Direction.TopRight] * radius;

            for(int dir = 0; dir < 6; dir++) {
                for(int i = 0; i < radius; i++) {
                    currCoordinate += Utils.Vectors[dir];   
                    activatedTiles.Add(currCoordinate, true);
                }   
            }
        }
    }
}