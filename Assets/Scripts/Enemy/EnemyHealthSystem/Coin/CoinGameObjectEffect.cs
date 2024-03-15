using UnityEngine;
using DG.Tweening;

public class CoinGameObjectEffect : MonoBehaviour
{
    private Transform _player;

    private bool _initialized;

    public void Initialize(Transform player)
    {
        if (_initialized == true)
        {
            return;
        }

        _player = player;

        _initialized = true;

        transform.DOMove(_player.position, 0.5f).OnKill(() => { Destroy(gameObject); });
    }
}