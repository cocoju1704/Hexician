using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
public class Variables {
    public List<Modifier> modifiers;

    public UnityEvent OnNumberChange;

    int _num;
    public int num {
        get {
            int ret = _num;
            foreach(Modifier modifier in modifiers) {
                ret = Utils.Cacluate(ret, modifier.op, modifier.num);
            }

            return ret;
        }
    }

    public Variables(int num) {
        this._num = num;

        modifiers = new List<Modifier>();
        OnNumberChange = new UnityEvent();
    }

    public void AddModifier(Modifier modifier) {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(Modifier modifier) {
        modifiers.Remove(modifier);
    }

    public void RemoveModifiersFromSource(object src) {
        int count = modifiers.Count;

        for(int i = count - 1; i >= 0; i--) {
            if(modifiers[i].src == src) {
                modifiers.RemoveAt(i);
            }
        }
    }

    public Variables Clone() {
        Variables newVar = new Variables(_num);

        foreach(Modifier modifier in modifiers) {
            newVar.modifiers.Add(modifier.Clone());
        }

        return newVar;
    }
}