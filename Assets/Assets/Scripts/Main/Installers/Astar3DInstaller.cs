using UnityEngine;
using BaseSystems.DesignPatterns.DependencyInjection;
using PathFinding.PathFinder;
using PathFinding;
using Main.Player;

namespace Main.Installers
{
    public class Astar3DInstaller: MonoInstaller
    {
        [SerializeField]
        private PlayerWaypointsMovement.Setting playerMovementSetting;

        [SerializeField]
        private PlayerInputHandler.Setting playerInputSetting;

        [SerializeField]
        private GridGraph.Setting gridGraphSetting;

        public override void InstallBindings()
        {
            Container.Bind<IPathFinder>().To<Astar>().AsSingle();
            Container.Bind<ITickable>().To<PlayerInputHandler>().AsSingle();

            Container.Bind<PlayerWaypointsMovement.Setting>().FromInstance(playerMovementSetting);
            Container.Bind<PlayerInputHandler.Setting>().FromInstance(playerInputSetting);
            Container.Bind<GridGraph.Setting>().FromInstance(gridGraphSetting);

            Container.Bind<GridGraph>().AsSingle().NonLazy();
            Container.Bind<PlayerWaypointsMovement>().AsSingle().NonLazy();            
        }
    }
}
