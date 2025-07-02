using UnityEngine;
using Zenject;
using Inventory;

namespace Installers
{
    public class InventoryServiceInstaller : MonoInstaller<InventoryServiceInstaller>
    {
        [SerializeField]
        private InventoryService inventoryService;
        
        public override void InstallBindings()
        {
            Container.Bind<InventoryService>()
                .FromInstance(inventoryService)
                .AsSingle();
        }
    }
}