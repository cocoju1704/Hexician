using System.Collections.Generic;
using Unity.Mathematics;

public class CreateHexTask : BattleTask {
    public BattleTile tile;
    public List<BattleTile> tiles;
    public Hex hex;
    public Unit src;
    public bool isLeaf;

    public CreateHexTask(BattleTile tile, Hex hex, Unit src = null) {
        this.tile = tile;
        this.hex = hex;
        this.src = src;
        isLeaf = false;
    }
    public CreateHexTask(List<BattleTile> tiles, Hex hex, Unit src = null) {
        this.tiles = tiles;
        this.hex = hex;
        this.src = src;
        isLeaf = false;
    }

    public override void Execute() {
        if(isLeaf) {
            if(!tile.isEmpty) {
                return;
            }

            Hex hexToCreate = hex.Clone();
            tile.AssignHex(hexToCreate)
                .Operate();
        }
        else {
            tiles ??= register.tiles;
            
            foreach(BattleTile tile in tiles) {
                if(tile.isEmpty) {
                    BattleManager.instance.AddTask(new CreateHexTask(tile, hex, src), register);
                }
            }
        }
    }

    protected override BattleTask CloneTask() {
        if(isLeaf) {
            return new CreateHexTask(tile, hex, src);
        }
        else {
            return new CreateHexTask(tiles, hex, src);
        }
    }
}
    