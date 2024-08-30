using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour, ISavable {
    public Room BuildRoom(Room room) {
        switch(room.roomType) {
            case Enums.RoomType.Empty :
            case Enums.RoomType.Elite :
            case Enums.RoomType.Boss :
            case Enums.RoomType.Danger :
                // room = BuildBattleRoom(room.roomType);
                break;
            case Enums.RoomType.Chest :
                room = BuildChestRoom(room);
                break;
            case Enums.RoomType.Shop :
                room = BuildShopRoom(room);
                break;
            case Enums.RoomType.Encounter :
                // room = BuildEncounterRoom();
                break;
            default : 
                // room = new Room();
                break;
            case Enums.RoomType.Debug :
                room = BuildDebugRoom(room);
                break;
        }

        room.isBuilt = true;
        return room;
    }

    Room BuildBattleRoom(Room room) {
        return null;
    }

    Room BuildChestRoom(Room room) {
        room.itemIds = new List<int> {1, 2, 3};
        return room;
    }

    Room BuildShopRoom(Room room) {
        // 디버깅용-시작
        room.itemIds = new List<int>{1, 2, 3, 4, 5, 6, 1, 1, 1, 1};
        // 디버깅용-끝
        return room;        
    }

    Room BuildEncounterRoom() {
        return null;
    }
    Room BuildDebugRoom(Room room) {
        return room;
    }
    public void Save(GameData gameData) {
        // throw new System.NotImplementedException();
    }

    public void Load(GameData gameData) {
        // throw new System.NotImplementedException();
    }
}
