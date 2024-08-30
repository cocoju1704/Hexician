using System.Collections.Generic;

public class Modifier {
    Enums.MathOperator _op;
    public Enums.MathOperator op {
        get {
            return _op;
        }
    }
    
    int _num;
    public int num {
        get {
            return _num;
        }
    }

    object _src;
    public object src {
        get {
            return _src;
        }
    }

    public Modifier(Enums.MathOperator op, int num, object src) {
        _op = op;
        _num = num;
        _src = src;
    }
    
    public Modifier Clone() {
        return new Modifier(_op, _num, src);
    }
}