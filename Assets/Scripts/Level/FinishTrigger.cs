using UnityEngine;
using System;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class FinishTrigger : MonoBehaviour
{
    public event Action Finished;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHitBox>() != null)
        {
            Finished?.Invoke();
        }
    }
}