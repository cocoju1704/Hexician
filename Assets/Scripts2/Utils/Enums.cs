public static class Enums {
    public enum Direction {
        BottomRight,
        BottomLeft,
        Left,
        TopLeft,
        TopRight,
        Right,
        None,
    }
    
    public enum CharacterType {
        None,
        Grass,
        Fire,
        Water
    }


    public enum HexState {
        Operating,
        Destroyed,
        Dismounted,
        Excluded
    }

    public enum MathOperator {
        Assignment,
        Plus,
        Minus,
        Mul,
        Div,
        Percent,
    }

    public enum ComparisonOperator {
        EQ,
        NE,
        LT,
        LE,
        GT,
        GE
    }

    public enum DamageType {
        Normal,
        True,
        Reflected,
        Loss,
    }

    public enum RoomType {
        Empty,
        Elite,
        Boss,
        Battle,
        Danger,
        Chest,
        Shop,
        Encounter,
        Upgrade,
        Stair,
        Debug,
    }

    public enum ChoiceType {
        AllRandom,
        OnlyAttack,
        OnlyAssist,
        OnlyOngoing,
        OneEach,
    }
    
    public enum StagePhase {
        PlayerSelect, // 플레이어가 이동할 타일 선택 단계
        PlayerAnimate, // 플레이어 움직이는 단계
        EnterRoom, // 방 입장 단계
        InRoom, // 방 내부 단계
        ExitRoom, // 방 이후 단계
        DisableTiles, // 타일 바활성화 단계
        ToNextTurn, // 다음 턴으로 넘어가는 단계
    }
    public enum ItemClass {
        Common,
        Rare,
        Epic,
        Legendary,
        Boss,
        Negative,
    }
}