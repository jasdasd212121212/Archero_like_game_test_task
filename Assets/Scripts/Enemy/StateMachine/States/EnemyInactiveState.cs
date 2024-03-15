public class EnemyInactiveState : State
{
    private IActiveAIControllable _ai;

    public EnemyInactiveState(IActiveAIControllable ai)
    {
        _ai = ai;
    }

    public override void Entry()
    {
        _ai.SetAiEnabled(false);
    }

    public override void Exit()
    {
        _ai.SetAiEnabled(true);
    }
}