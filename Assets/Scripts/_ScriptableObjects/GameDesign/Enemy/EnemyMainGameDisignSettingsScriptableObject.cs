using UnityEngine;

[CreateAssetMenu(fileName = "MainSettings", menuName = "Game design/Enemy/MainSettings")]
public class EnemyMainGameDisignSettingsScriptableObject : ScriptableObject
{
    [SerializeField][Min(1)] private float _moveSpeed = 10;
    [SerializeField][Min(2)] private int _enemyHealth = 100;

    public float EnemyMoveSpeed => _moveSpeed;
    public int EnemyHealth => _enemyHealth;
}