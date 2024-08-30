using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hex {
    public Enums.HexState state;
    public Unit owner;

    public int id;
    public string name;
    public Sprite sprite;
    public string description;
    public int price;
    public HexStat stat;
    public Scope scope;
    public List<Trigger> triggers;
    public List<Condition> conditions;
    public List<HexAbility> abilities;
    public Register register;
    public BattleTile tilePlaced;
    public List<BattleTile> tilesInScope;    

    public Hex(int id, string name, Sprite sprite, string description, 
        HexStat stat, Scope scope, 
        List<Trigger> triggers, 
        List<Condition> conditions, 
        List<HexAbility> abilities) {

        for(int i = 0; i < triggers.Count; i++) {
            triggers[i].SetCondition(conditions[i]);
            triggers[i].SetFunc(abilities[i].Execute);
        }

        state = Enums.HexState.Destroyed;
        this.id = id;
        this.name = name;
        this.sprite = sprite;
        this.description = description;
        this.stat = stat;
        this.scope = scope;

        this.triggers = triggers;
        this.conditions = conditions;
        this.abilities = abilities; 
        this.register = new Register();
    }    

    public void SetTilePlaced(BattleTile tile) {
        this.tilePlaced = tile;
        this.tilesInScope = scope.GetTilesInScope(tile);
    }
    public void ChangeTiles(Enums.Direction dir) {
        scope.SetDirections(dir);
        this.tilesInScope = scope.GetTilesInScope(tilePlaced);
    }

    public Hex SetState(Enums.HexState state) {
        this.state = state;

        return this;
    }

    public void AddTrigger(Trigger trigger) {        
        triggers.Add(trigger);
        trigger.Activate(owner, this);
    }

    public void RemoveTrigger(Trigger trigger) {
        trigger.Deactivate();
        triggers.Remove(trigger);
    }

    public void Operate(Unit owner = null) {
        if(tilePlaced == null) {
            return;
        }

        this.owner = owner ?? BattleManager.instance.p;
        state = Enums.HexState.Operating;
        
        ActivateTriggers();
        ActivateAura();
    }

    public void Destroy() {
        this.state = Enums.HexState.Destroyed;
        
        this.tilePlaced = null;
        this.tilesInScope = null;
    }

    public Hex ActivateTriggers(bool applysOnlyLifeTrigger = false) {
        foreach(Trigger trigger in triggers) {
            if(applysOnlyLifeTrigger && !trigger.isLifeTrigger) {
                continue;
            }

            trigger.Activate();
        }

        return this;
    }

    public Hex DeactivateTriggers(bool exceptsLifeTrigger = false) {
        foreach(Trigger trigger in triggers) {
            if(exceptsLifeTrigger && trigger.isLifeTrigger) {
                continue;
            }

            trigger.Deactivate();
        }

        return this;
    }

    public Hex ActivateAura() {
        //
        return this;
    }

    public Hex DeactivateAura() {
        return this;
    }

    public Hex Clone() {
        Dictionary<Enums.HexState, Ability> newAbilitiesWhen = new Dictionary<Enums.HexState, Ability>();
        foreach(KeyValuePair<Enums.HexState, Ability> sa in newAbilitiesWhen) {
            newAbilitiesWhen.Add(sa.Key, sa.Value.Clone());
        }

        return new Hex(this.id, name, sprite, description, 
            stat.Clone(), scope.Clone(), 
            triggers.ConvertAll(t => t.Clone()),
            conditions.ConvertAll(c => c?.Clone()),
            abilities.ConvertAll(a => a.Clone()));
    }
}