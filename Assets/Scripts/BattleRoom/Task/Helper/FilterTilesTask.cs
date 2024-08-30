using System.Collections.Generic;
using System.Linq;

public class FilterTilesTask : BattleTask {
    public List<BattleTile> tiles;
    public List<Condition> conditions;

    public FilterTilesTask(List<BattleTile> tiles, Condition condition)
        : this(tiles, new List<Condition> {condition}) {}

    public FilterTilesTask(List<BattleTile> tiles, List<Condition> conditions) {
        this.tiles = tiles;
        this.conditions = conditions;
    }

    public override void Execute() {
        tiles ??= register.tiles;

        List<BattleTile> filteredTiles = tiles.Where((tile) => {
            bool isMatched = true;

            foreach(Condition condition in conditions) {
                if(!condition.Validate(tile)) {
                    isMatched = false;
                    break;
                }
            }

            return isMatched;
        }).ToList();

        register.tiles = filteredTiles;
    }

    protected override BattleTask CloneTask() {
        return new FilterTilesTask(tiles, conditions);
    }
}