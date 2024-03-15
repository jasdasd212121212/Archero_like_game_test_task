using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerInstallationMark _playerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInstallationMark>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
    }
}