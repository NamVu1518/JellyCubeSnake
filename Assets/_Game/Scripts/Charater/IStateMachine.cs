using System;

public interface IStateMachine<T>
{
    public void OnIn(T t);

    public void OnRun(T t);

    public void OnOut(T t);
}
