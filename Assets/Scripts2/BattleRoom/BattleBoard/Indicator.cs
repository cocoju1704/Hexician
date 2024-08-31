using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Indicator {
    [Header("설정")]
    public bool isBorderTaret;
    public Color color;

    public Indicator(): this(true, Color.yellow) {}
    public Indicator(bool isBorderTaret) : this(isBorderTaret, Color.yellow) {}
    public Indicator(bool isBorderTaret, Color color) {
        this.isBorderTaret = isBorderTaret;
        this.color = color;
    }

    List<BattleTile> tiles;
    public void TurnOn(List<BattleTile> tiles) {
        if(this.tiles != null) {
            TurnOff();
        }
        
        this.tiles = tiles;
        foreach(BattleTile tile in tiles) {
            if(isBorderTaret) {
                tile.TurnOnBorderHighlight(color);
            }
            else {
                tile.TurnOnHighlight(color);
            }
        }
    }

    public void TurnOn(BattleTile tile, Scope scope) {
        List<BattleTile> tiles = scope.GetTilesInScope(tile);
        this.TurnOn(tiles);
    }

    public void TurnOff() {
        if(tiles == null) {
            return;
        }
        
        foreach(BattleTile tile in tiles) {
            if(isBorderTaret) {
                tile.TurnOffBorderHighlight();

            }
            else {
                tile.TurnOffHighlight();
            }
        }

        tiles = null;
    }
}
