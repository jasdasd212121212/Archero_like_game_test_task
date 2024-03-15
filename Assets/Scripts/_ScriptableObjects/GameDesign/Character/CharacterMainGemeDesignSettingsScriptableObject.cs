using UnityEngine;

[CreateAssetMenu(fileName = "MainSettings", menuName = "Game design/Character/MainSettings")]
public class CharacterMainGemeDesignSettingsScriptableObject : ScriptableObject
{
    [SerializeField][Min(1)] private float _moveSpeed = 10;
    [SerializeField][Min(2)] private int _playerHealth = 100;

    public float PlayerMoveSpeed => _moveSpeed;
    public int PlayerHealth => _playerHealth;
}