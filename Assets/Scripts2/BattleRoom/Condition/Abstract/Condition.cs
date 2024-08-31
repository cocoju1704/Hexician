using System;

public abstract class Condition {
    public abstract bool Validate(object param =null);

    public abstract Condition Clone(); 
}