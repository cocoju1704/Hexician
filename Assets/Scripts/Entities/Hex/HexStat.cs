using System;
using Unity.VisualScripting;

[Serializable]
public class HexStat {
    public Variables damageVar;
    public int damage {
        get {
            return damageVar.num;
        }
    }

    public Variables healVar;
    public int heal {
        get {
            return healVar.num;
        }
    }

    public Variables armorVar;
    public int armor {
        get {
            return armorVar.num;
        }
    }

    public Variables magicNumVar1;
    public int magicNum1 {
        get {
            return magicNumVar1.num;
        }
    }

    public Variables magicNumVar2;
    public int magicNum2 {
        get {
            return magicNumVar2.num;
        }
    }    

    public Variables magicNumVar3;
    public int magicNum3 {
        get {
            return magicNumVar3.num;
        }
    }

    public Variables valueVar;
    public int value {
        get {
            return valueVar.num;
        }
    }

    public HexStat() {}
    public HexStat(int damage, int heal, int armor, int magicNum1, int magicNum2, int magicNum3) {
        damageVar = new Variables(damage);
        healVar = new Variables(heal);
        armorVar = new Variables(armor);
        magicNumVar1 = new Variables(magicNum1);
        magicNumVar2 = new Variables(magicNum2);
        magicNumVar2 = new Variables(magicNum2);
    }

    public HexStat Clone() {
        HexStat newStat = new HexStat();

        newStat.damageVar = this.damageVar.Clone(); 
        newStat.healVar = this.healVar.Clone();
        newStat.armorVar = this.armorVar.Clone();
        newStat.magicNumVar1 = this.magicNumVar1.Clone();
        newStat.magicNumVar2 = this.magicNumVar2.Clone();
        newStat.magicNumVar3 = this.magicNumVar3.Clone();
        newStat.valueVar = this.valueVar.Clone();

        return newStat;
    }
}