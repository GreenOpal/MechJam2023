using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechJam
{
    public class PlayerInputController : ConfigurableBehaviour
    {
        private PlayerInput _input;
        [SerializeField] private Rigidbody2D _rb;


        public override void StartGame()
        {
            base.StartGame();

            _input = new PlayerInput();
            _input.Player.Enable();
        }

        void Update()
        {
            if (!IsPlaying) return;
            Vector2 movement = GetPlayerMovement();
            MovePlayer(movement);
        }

        private void MovePlayer(Vector2 movement)
        {
            if (Mathf.Abs(movement.x) < Mathf.Epsilon && Mathf.Abs(movement.y) < Mathf.Epsilon) return;
            var scaledMovement = (_config._PlayerSpeed * Time.deltaTime * movement);
            _rb.MovePosition( (Vector2)transform.position + scaledMovement);
        }

        private Vector2 GetPlayerMovement()
        {
            var moveVal = _input.Player.Move.ReadValue<Vector2>();
            return moveVal;
        }
    }
}
