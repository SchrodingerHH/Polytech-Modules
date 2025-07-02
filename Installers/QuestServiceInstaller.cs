using UnityEngine;
using Zenject;
using Quest;

namespace Installers
{
    public class QuestServiceInstaller : MonoInstaller<QuestServiceInstaller>
    {
        [SerializeField]
        private QuestService questService;
        
        public override void InstallBindings()
        {
            Container.Bind<QuestService>()
                .FromInstance(questService)
                .AsSingle();
        }
    }
}