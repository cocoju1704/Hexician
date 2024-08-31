using System.Collections.Generic;
using UnityEngine;

public class HexOutTask : BattleTask {
    public List<BattleTile> tiles;
    public Enums.HexState state;
    public Unit src;
    public Hex srcHex;

    public HexOutTask(BattleTile tile, Enums.HexState state, Unit src = null, Hex srcHex = null) 
        : this(new List<BattleTile> { tile }, state, src, srcHex) {
        if(tile == null) {
            tiles = null;
        }
    }

    public HexOutTask(List<BattleTile> tiles, Enums.HexState state, Unit src = null, Hex srcHex = null) {
        this.src = src;
        this.srcHex = srcHex;
        this.tiles = tiles;

        this.state = state;

        if(this.state != Enums.HexState.Dismounted && this.state != Enums.HexState.Excluded) {
            this.state = Enums.HexState.Dismounted;
        }
    }

    public override void Execute() {
        tiles ??= register.tiles;

        register.nums[0] = 0;
        
        foreach(BattleTile tile in tiles) {
            if(!tile.isEmpty) {
                tile.hex.SetState(state).DeactivateTriggers(exceptsLifeTrigger: true);
                
                BattleManager.instance.taskManager.hexesToDestroy.Add(tile.hex);

                BattleManager.instance.AddTask(new DetachHexTask(tile, src, srcHex), register);
            }
        }
    }

    protected override BattleTask CloneTask() {
        return new HexOutTask(tiles, state, src, srcHex);
    }
}
