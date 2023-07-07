using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechJam
{
    public class PlayerInputController : ConfigurableBehaviour
    {
        [SerializeField] private PlayerInput _input;

        public override void StartGame()
        {
            base.StartGame();
                
            _input.Player.Enable();

            Debug.LogWarning("get in there");
        }

        void Update()
        {
            if (!IsPlaying) return;
            Vector2 movement = GetPlayerMovement();
        }

        private Vector2 GetPlayerMovement()
        {
            var moveVal = _input.Player.Move.ReadValue<Vector2>();
            Debug.LogWarning(moveVal);
            return moveVal;
        }
    }
}
