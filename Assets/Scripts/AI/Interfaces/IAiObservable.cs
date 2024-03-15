using System;

public interface IAiObservable
{
    public event Action MoveStarted;
    public event Action MoveEnded;
}