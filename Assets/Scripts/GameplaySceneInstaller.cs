using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField]
    private GameManager gameManager;

    public override void InstallBindings()
    {
        Container.Bind<IGameManager>().To<GameManager>().FromInstance(gameManager).AsSingle();
    }
}