using System.Collections.Generic;
using UnityEngine;

public class DetachHexTask : BattleTask {
    public BattleTile tile;
    public Unit src;
    public Hex srcHex;

    bool isLeaf;

    public DetachHexTask(BattleTile tile, Unit src = null, Hex srcHex = null) {
        this.tile = tile;

        this.src = src;
        this.srcHex = srcHex;
    }

    public DetachHexTask(Unit src, BattleTile tile)
        : this(src, null, tile) {}
    public DetachHexTask(Unit src, Hex srcHex, BattleTile tile) {
        this.src = src;
        this.srcHex = srcHex;
        this.tile = tile;
    }
    
    public override void Execute() {
        Hex hex = tile.hex;

        // Register에 저장
        register.nums[0] += hex.stat.value;

        MoveCard(tile.card, hex.state);
        // hex와 tile을 분리
        tile.DetachHex();
    }

    void MoveCard(Card card, Enums.HexState state) {
        if(card == null) {
            return;
        }

        if(state == Enums.HexState.Dismounted) {
            BattleManager.instance.cardManager.MoveBoardToGraveyard(card);
        }
        else if(state == Enums.HexState.Excluded) {
            BattleManager.instance.cardManager.MoveBoardToVoid(card);
        }
    }

    protected override BattleTask CloneTask() {
        return new DetachHexTask(tile, src, srcHex);
    }
}