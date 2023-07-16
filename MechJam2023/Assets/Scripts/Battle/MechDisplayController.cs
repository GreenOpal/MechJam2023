using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MechJam
{
    public class MechDisplayController : BattleConfigurableBehaviour
    {

        [SerializeField] private MechView _playerView;
        [SerializeField] private MechView _enemyView;

        public override void Initialize(GameControllerBase controller)
        {
            base.Initialize(controller);

            _battleController.OnPlayerMechAssigned += (mech) => _playerView.DisplayMech(mech);
            _battleController.OnEnemyMechAssigned += (mech) => _enemyView.DisplayMech(mech);

            _battleController.OnMechAttacks += DisplayAttack;
            _battleController.OnMechWasAttacked += DisplayGetHit;
        }

        private void DisplayGetHit(Mech mech, MechPart _)
        {
            if (mech.Name == _playerView.MechName)
            {
                _playerView.GetHit();
            }
            else
            {
                _enemyView.GetHit();
            }
        }

        private void DisplayAttack(Mech mech)
        {
            if (mech.Name == _playerView.MechName)
            {
                _playerView.Attack();
            } else
            {
                _enemyView.Attack();
            }
        }
    }
}
