using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HitBox : MonoBehaviour, IDamageble
{
    public event Action<int> Damaged;

    public void TakeDamage(int damage)
    {
        Damaged?.Invoke(damage);
    }

#if UNITY_EDITOR
    private const int DEBUG_DAMAGE = 30;

    [ContextMenu("Debug -> damge")]
    private void DebugDamge()
    {
        if (Application.isPlaying == false)
        {
            return;
        }

        TakeDamage(DEBUG_DAMAGE);
    }
#endif
}