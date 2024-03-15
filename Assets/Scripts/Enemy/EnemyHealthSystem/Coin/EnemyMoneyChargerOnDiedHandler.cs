using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyMoneyChargerOnDiedHandler : MonoBehaviour
{
    [SerializeField] private CoinGameObjectEffect _coinEffctObject;
    [SerializeField][Min(1)] private int _killCost = 10;

    private EnemyHealth _health;
    private Transform _player;

    [Inject]
    private void Construct(PlayerInstallationMark player)
    {
        _player = player.PlayerTransform;
    }

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();

        _health.Died += ChargeMoney;
    }
     
    private void OnDestroy()
    {
        _health.Died -= ChargeMoney;
    }

    private void ChargeMoney()
    {
        Wallet.ChargeMoney(_killCost);
        Instantiate(_coinEffctObject, transform.position, Quaternion.identity).Initialize(_player);
    }
}