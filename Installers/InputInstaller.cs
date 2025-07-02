using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Player;

namespace Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerInput playerInput;
        private PlayerInputHandler _playerInputHandler;

        public override void InstallBindings()
        {
            _playerInputHandler = new PlayerInputHandler(playerInput);
            Container.Bind<PlayerInputHandler>()
                .FromInstance(_playerInputHandler)
                .AsSingle();
        }
    }
}