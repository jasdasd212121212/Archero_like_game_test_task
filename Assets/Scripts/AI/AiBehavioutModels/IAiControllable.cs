using UnityEngine;

public interface IAiControllable
{
    void Initialize(Transform target, float moveSpeed);
    void StartMove();
    void StopMove();
}