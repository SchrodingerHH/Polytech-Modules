using UnityEngine;
using Zenject;
using Dialogue;

namespace Installers
{
    public class DialogueServiceInstaller : MonoInstaller
    {
        [SerializeField]
        private DialogueService dialogueService;
        
        public override void InstallBindings()
        {
            Container.Bind<DialogueService>()
                .FromInstance(dialogueService)
                .AsSingle();
        }
    }
}