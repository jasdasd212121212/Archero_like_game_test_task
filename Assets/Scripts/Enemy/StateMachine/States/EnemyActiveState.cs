public class EnemyActiveState : State
{
    private IActiveAIControllable _ai;
    
    public EnemyActiveState(IActiveAIControllable ai)
    {
        _ai = ai;
    }

    public override void Entry()
    {
        _ai.SetAiEnabled(true);
    }

    public override void Exit()
    {
        _ai.SetAiEnabled(false);
    }
}