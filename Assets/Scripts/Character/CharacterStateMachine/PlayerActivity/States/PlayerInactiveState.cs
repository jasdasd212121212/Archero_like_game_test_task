public class PlayerInactiveState : State
{
    private CharacterRotaterStateMachine _characterStateMachine;
    private PlayerMover _playerMover;
    private CharacterAutomaticlyShooter _characterAutomaticlyShooter;

    public PlayerInactiveState(CharacterRotaterStateMachine characterStateMachine, PlayerMover playerMover, CharacterAutomaticlyShooter shooter)
    {
        _characterStateMachine = characterStateMachine;
        _playerMover = playerMover;
        _characterAutomaticlyShooter = shooter;
    }

    public override void Entry()
    {
        _characterStateMachine.SetActive(false);
        _playerMover.enabled = false;
        _characterAutomaticlyShooter.enabled = false;
    }

    public override void Exit()
    {
        _characterStateMachine.SetActive(true);
        _playerMover.enabled = true;
        _characterAutomaticlyShooter.enabled = true;
    }
}