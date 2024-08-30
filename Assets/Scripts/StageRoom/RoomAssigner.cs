// using UnityEngine;
// using System.Collections.Generic;
// using System;
// public class RoomAssigner : MonoBehaviour {
//     [SerializeField] public LegacyStageBoard stageBoard;
//     Dictionary<int, List<Vector3Int>> keysByRadius = new();

//     void Awake() {

//     }
//     public void SetRooms()
//     {
//         //72개의 방 - 보스1개 - 플레이어1개 - 상점+보물 5개 - 포탈2개 - 보스근처3개
//         SetRoomsBasic();
//         SetChestRooms();
//         SetShopRooms();
//         SetBossRoom();
//         SetUpgradeRoom();
//         SetStairRoom();
//     }
//     void SetRoomsBasic() {
//         List<Vector3Int> keys = new(stageBoard.tiles.Keys);
//         foreach (Vector3Int key in keys)
//         {
//             int radius = key.y;
//             if (!keysByRadius.ContainsKey(radius))
//             {
//                 keysByRadius[radius] = new List<Vector3Int>();
//             }
//             keysByRadius[radius].Add(key);
//         }
//         foreach (var kvp in keysByRadius)
//         {
//             foreach (Vector3Int key in kvp.Value)
//             {
//                 int prob = Randoms.GetWeightedRandomOption(new List<int>{7, 3});
//                 if (prob == 0) {
//                     stageBoard.tiles[key].roomType = Enums.RoomType.Empty;
//                 } else {
//                     stageBoard.tiles[key].roomType = Enums.RoomType.Encounter;
//                 }
//             }
//         }
//     }
//     void SetShopRooms() {
//         StageAreas.shopAreas.ForEach(shopArea => {
//             Vector3Int pos = Randoms.SelectRandomTile(shopArea);
//             while (!IsIgnorableRoom(stageBoard.tiles[pos].roomType))
//             {
//                 pos = Randoms.SelectRandomTile(shopArea);
//             }
//             stageBoard.tiles[pos].roomType = Enums.RoomType.Shop;
//         });
//     }
//     void SetChestRooms() {
//         StageAreas.shopAreas.ForEach(shopArea => {
//             Vector3Int pos = Randoms.SelectRandomTile(shopArea);
//             while (!IsIgnorableRoom(stageBoard.tiles[pos].roomType))
//             {
//                 pos = Randoms.SelectRandomTile(shopArea);
//             }
//             stageBoard.tiles[pos].roomType = Enums.RoomType.Chest;
//         });
//     }
//     void SetBossRoom() {
//         Vector3Int bossPos = -stageBoard.playerStartPos;
//         stageBoard.tiles[bossPos].roomType = Enums.RoomType.Boss;
//     }
//     void SetUpgradeRoom() {
//         foreach (Vector3Int pos in StageAreas.upgradeArea) {
//             stageBoard.tiles[pos].roomType = Enums.RoomType.Upgrade;
//         }
//     }
//     void SetStairRoom() {
//         Vector3Int pos = Randoms.SelectRandomTile(keysByRadius[stageBoard.maxRadius]);
//         while (!IsIgnorableRoom(stageBoard.tiles[pos].roomType))
//         {
//             pos = Randoms.SelectRandomTile(keysByRadius[stageBoard.maxRadius]);
//         }
//         stageBoard.tiles[pos].roomType = Enums.RoomType.Stair;
//         Vector3Int downPos = new Vector3Int(-pos.z, -pos.y, -pos.x);
//         stageBoard.tiles[downPos].roomType = Enums.RoomType.Stair;
//     }
//     bool IsIgnorableRoom(Enums.RoomType roomType) {
//         return roomType == Enums.RoomType.Empty || roomType == Enums.RoomType.Encounter;
//         return false;
//     }
// }