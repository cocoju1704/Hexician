using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TCA {
    [SerializeReference, SubclassSelector]
    public Trigger trigger;
    [SerializeReference, SubclassSelector]
    public Condition condition;
    [SerializeReference, SubclassSelector]
    public HexAbility ability;
}

[CreateAssetMenu(fileName ="SOHexData", menuName = "Scriptable Object/Hex", order = 0)]
public class SOHexData : ScriptableObject {
    public int id;
    public new string name;
    public Sprite sprite;
    public string description;

    public int damage;
    public int heal;
    public int armor;
    public int magicNum1;
    public int magicNum2;
    public int magicNum3;

    public Scope scope;
    public List<TCA> tcas;
    public Hex CreateHex() {
        HexStat stat = new HexStat(damage, heal, armor, magicNum1, magicNum2, magicNum3);

        Hex hex = new Hex(id, name, sprite, description, 
            stat, scope.Clone(),
            tcas.ConvertAll(tca => tca.trigger.Clone()),
            tcas.ConvertAll(tca => tca.condition?.Clone()),
            tcas.ConvertAll(tca => tca.ability?.Clone()));

        return hex;
    }
}