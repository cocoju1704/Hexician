using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Scope {
    [SerializeField]
    public int distance = 1;
    [SerializeField]
    List<Enums.Direction> directions;

    Enums.Direction originalDir;
    List<int> diff;

    public Scope(int distance, List<Enums.Direction> directions) {
        this.distance = distance;
        this.directions = directions;

        diff = new List<int>();

        for(int i = 0; i < directions.Count; i++) {
            diff.Add(directions[i] - directions[0]);
            if(i == 0) {
                originalDir = directions[i];
            }
        }
    }
    
    public void Cancel() {
        SetDirections(originalDir);
    }

    public void SetDirections(Enums.Direction dir) {
        for(int i = 0; i < directions.Count; i++) {
            directions[i] = (Enums.Direction) (((int) dir + diff[i] + 6) % 6);
        }
    }

    public List<BattleTile> GetTilesInScope(BattleTile tile) {
        if(tile == null) {
            return null;
        }

        BattleBoard board = BattleManager.instance.board;
        List<BattleTile> tiles = new List<BattleTile>(); 

        if(distance >= 10) {
            tiles = board.GetAllTiles();
            tiles.Remove(tile);
            return tiles;
        }
        else {
            BattleTile currTile;
            Vector3Int currHexCoodinate;

            for(int dist = 1; dist <= distance; dist++) {
                foreach(Enums.Direction dir in directions) {
                    currHexCoodinate = tile.coordinate + Utils.Vectors[(int) dir] * dist;

                    if(board.TryGetTile(currHexCoodinate, out currTile)) {
                        tiles.Add(currTile);
                    }
                }
            }

            return tiles;
        }
    }

    public Scope Clone() {

        return new Scope(this.distance, this.directions.ToList());
    }
}
