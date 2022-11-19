using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ForestReturn.Scripts.PlayerScripts
{
    public class PlayerManager : BaseCharacter
    {
        private InputHandler _inputHandler;
        private PlayerLocomotion _playerLocomotion;
        [HideInInspector] public PlayerInput playerInput;
        private Animator _animator;


        protected override void Awake()
        {
            base.Awake();
            playerInput = GetComponent<PlayerInput>();
            _inputHandler = GetComponent<InputHandler>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
            _animator = GetComponentInChildren<Animator>();
        }

        public void Init()
        {
            _playerLocomotion.Init();
        }

        private void Update()
        {
            _inputHandler.isInteracting = _animator.GetBool("isInteracting");
            _inputHandler.rollFlag = false;
        }
    }
}