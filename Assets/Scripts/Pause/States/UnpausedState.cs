using UnityEngine;

public class UnpausedState : State
{
    public override void Entry()
    {
        Time.timeScale = 1f;
    }

    public override void Exit()
    {
        Time.timeScale = 0f;
    }
}