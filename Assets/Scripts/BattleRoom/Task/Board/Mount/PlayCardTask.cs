public class PlayCardTask : BattleTask {
    public Unit src;
    public Card card;
    public Hex hex;
    public BattleTile tile;

    public PlayCardTask(Card card, BattleTile tile) {
        this.src = BattleManager.instance.p;
        this.card = card;
        this.hex = card.hex;
        this.tile = tile;
    }

    public override void Execute() {
        if(!tile.isEmpty) {
            return;
        }

        // 타일에 기어를 할당, 생명과 관련된 트리거만 활성화
        tile.AssignHex(hex)
            .ActivateTriggers(applysOnlyLifeTrigger: true);

        // 카드를 핸드 -> 보드
        BattleManager.instance.cardManager.MoveHandToBoard(card);
        // 핸드 정렬 애니메이션
        BattleManager.instance.AddSeq(MyAnim.instance.GetAlignHandAnim(0.3f));
    }


    protected override BattleTask CloneTask() {
        return new PlayCardTask(card, tile);
    }
}