using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{    
    public override void InstallBindings()
    {
        Container.Bind<IUnitsManager>().To<UnitsManager>().FromNew().AsSingle().NonLazy();
    }
}