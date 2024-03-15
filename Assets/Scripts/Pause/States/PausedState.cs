using UnityEngine;

public class PausedState : State
{
    public override void Entry()
    {
        Time.timeScale = 0f;
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
    }
}