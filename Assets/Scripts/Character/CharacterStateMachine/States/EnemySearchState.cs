public class EnemySearchState : State
{
    private CharacterMoveRotationHandler _characterMoveRotationHandler;
    private CharacterAutomaticlyShooter _characterAutomaticlyShooter;

    public EnemySearchState(CharacterMoveRotationHandler character, CharacterAutomaticlyShooter characterAutomaticlyShooter)
    {
        _characterMoveRotationHandler = character;
        _characterAutomaticlyShooter = characterAutomaticlyShooter;
    }

    public override void Entry()
    {
        _characterMoveRotationHandler.enabled = false;
        _characterAutomaticlyShooter.enabled = true;
    }

    public override void Exit()
    {
        _characterMoveRotationHandler.enabled = true;
        _characterAutomaticlyShooter.enabled = false;
    }
}