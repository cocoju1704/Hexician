using System.Collections.Generic;
using UnityEngine;

public class GameData {
    public Enums.CharacterType characterType;
    public PlayerData playerData;

    public int floorNo;
    public int turns;
    public  Dictionary<Vector3Int, Room> rooms;

    public GameData() : this(new PlayerData()) {}
    
    public GameData(PlayerData playerData) {
        characterType = Enums.CharacterType.None;
     
        this.playerData = new PlayerData();
     
        floorNo = 1;
        turns = 1;
        rooms = new Dictionary<Vector3Int, Room>();
    }

}