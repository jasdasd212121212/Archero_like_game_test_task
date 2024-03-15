using UnityEngine;

[CreateAssetMenu(fileName = "EnemyExploder", menuName = "Game design/Enemy/Exploder")]
public class EnemyExploderSettingsScriptableObject : ScriptableObject
{
    [SerializeField][Min(0.01f)] private float _explodeDellay = 1f;
    [SerializeField][Min(1)] private float _explodeDistance = 2f;
    [SerializeField][Min(1)] private int _explodeDamage = 50;

    public float ExplodeDellay => _explodeDellay;
    public float ExplodeDistance => _explodeDistance;
    public int ExplodeDamage => _explodeDamage;
}