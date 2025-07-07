using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Hero player;
    [SerializeField] private float playerSpeed = 5f;

    public override void InstallBindings()
    {
        Container.Bind<Hero>().FromInstance(player);
        Container.Bind<IInputStream>().To<InputSystem>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<ReactivePlayerMover>().AsSingle()
            .WithArguments(player.transform, playerSpeed);
        Container.BindInterfacesTo<CameraForHero>().FromComponentInHierarchy().AsSingle();
    }
}