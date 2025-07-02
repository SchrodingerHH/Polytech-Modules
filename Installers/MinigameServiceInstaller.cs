using UnityEngine;
using Zenject;
using Minigames;

namespace Installers
{
    public class MinigameServiceInstaller : MonoInstaller<MinigameServiceInstaller>
    {
        [SerializeField]
        private MinigameService minigameService;
        
        public override void InstallBindings()
        {
            Container.Bind<MinigameService>()
                .FromInstance(minigameService)
                .AsSingle();
        }
    }
}