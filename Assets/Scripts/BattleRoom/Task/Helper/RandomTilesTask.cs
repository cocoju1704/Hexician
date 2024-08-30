    using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomTilesTask : BattleTask {
    List<BattleTile> tiles;
    int? count;
  
    public RandomTilesTask(List<BattleTile> tiles, int? count) {
        this.tiles = tiles;
        this.count = count;
    }

    public override void Execute() {
        tiles ??= register.tiles;
        count ??= register.nums[0];

        tiles.Shuffle();

        register.tiles = tiles.Take((int) count).ToList();
    }

    protected override BattleTask CloneTask() {
        return new RandomTilesTask(tiles, count);
    }
}