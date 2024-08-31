using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomBuilder))]
[Serializable]
public class Stage : MonoBehaviour, ISavable {
    [SerializeField] int height = 7;
    [SerializeField] int width = 11;
    
    public int floorNo = 1;
    public int truns = 1; 
    public int enemyPursuitDelay = 0;
    Dictionary<Vector3Int, Room> rooms;
    RoomBuilder roomBuilder;

    void Start() {
        roomBuilder = GetComponent<RoomBuilder>();

        floorNo = 1;
        truns = 1;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.A)) {
            Debug.Log("A Pressed. Entering Debug Room");
            VisitRoom(Vector3Int.zero);
            SceneLoadSystem.instance.LoadScene(Enums.RoomType.Debug);
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            Debug.Log("B Pressed. Entering Boss Room");
            VisitRoom(Utils.Vectors[(int)Enums.Direction.Left]);
            SceneLoadSystem.instance.LoadScene(Enums.RoomType.Chest);}
    }
    public void CreateStage() {
        rooms = new Dictionary<Vector3Int, Room> {
            { Vector3Int.zero, new Room(Enums.RoomType.Debug)}
        };

        Vector3Int midCoordinate = Vector3Int.zero;
        for(int w = 1; w < width; w++) {
            midCoordinate += Utils.Vectors[(int)Enums.Direction.Left];
            rooms.Add(midCoordinate, new Room());

            for(int h = 1; h <= Math.Min(w, height / 2); h++) {
                Vector3Int coordinate = midCoordinate + (Utils.Vectors[(int)Enums.Direction.TopRight] * h);
                rooms.Add(coordinate, new Room());

                coordinate = midCoordinate + (Utils.Vectors[(int)Enums.Direction.BottomRight] * h);
                rooms.Add(coordinate, new Room());
            }
        }
        rooms[Utils.Vectors[(int)Enums.Direction.Left]].roomType = Enums.RoomType.Chest;
        rooms[Utils.Vectors[(int)Enums.Direction.TopLeft]].roomType = Enums.RoomType.Shop;
        rooms[Utils.Vectors[(int)Enums.Direction.BottomLeft]].roomType = Enums.RoomType.Battle;
    }

    public Room GetRoom(Vector3Int coordinate) {
        if(!rooms.ContainsKey(coordinate)) {
            return null;
        }

        return rooms[coordinate];
    }

    public Dictionary<Vector3Int, Room> GetAllRooms() {
        return rooms;
    }

    public Room VisitRoom(Vector3Int coordinate) {
        Room currentRoom;
        if(!rooms.TryGetValue(coordinate, out currentRoom)) {
            return null;
        }
        if(currentRoom.isVisited) {
            return null;
        }

        roomBuilder.BuildRoom(currentRoom);
        currentRoom.isVisited = true;

        return currentRoom;
    }

    public void LeaveRoom(Vector3Int coordinate) {

    }

    public void Load(GameData gameData) {
        this.floorNo = gameData.floorNo;
        this.truns = gameData.turns;
        this.rooms = new Dictionary<Vector3Int, Room>(gameData.rooms);
    }

    public void Save(GameData gameData) {
        gameData.floorNo = floorNo;
        gameData.turns = this.truns;
        gameData.rooms = new Dictionary<Vector3Int, Room> (this.rooms);    
    }
}