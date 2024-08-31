using System.Collections.Generic;
using UnityEngine;

public class CountTilesTask : BattleTask {
    List<BattleTile> tiles;
    int idxToStore;
    public CountTilesTask(List<BattleTile> tiles, int idxToStore) {
        this.tiles = tiles;
        this.idxToStore = idxToStore;
    }

    public override void Execute() {
        tiles ??= register.tiles;
        
        if(tiles == null) {
            register.nums[idxToStore] = 0;
        }
        else {
            register.nums[idxToStore] = tiles.Count;
        }
    }

    protected override BattleTask CloneTask() {
        return new CountTilesTask(tiles, idxToStore);
    }
}