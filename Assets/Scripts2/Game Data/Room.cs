using UnityEngine;
using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

public class Room {    
    public Enums.RoomType roomType;
    public bool isBuilt;
    public bool isVisited;
    // public Sprite backgroundSprite;

    #region Battle
    public List<int> monsterIds;
    //RewardData rewardData;
    #endregion

    #region Encounter
    public int encounterId;
    #endregion
    
    #region Shop
    public List<int> itemIds;
    public List<int> hexIds;
    public int shopType;
    #endregion
    
    #region Treasure
    public int itemId;
    #endregion

    public Room() {
        roomType = Enums.RoomType.Shop;
        monsterIds = new List<int>();
        itemIds = new List<int>();
        hexIds = new List<int>();
    }
    public Room(Enums.RoomType roomType) {
        this.roomType = roomType;
        monsterIds = new List<int>();
        itemIds = new List<int>();
        hexIds = new List<int>();

    }
}