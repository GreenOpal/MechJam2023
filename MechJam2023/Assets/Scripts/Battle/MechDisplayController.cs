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

            _battleController.OnMechAssigned += AssignMech;

            _battleController.OnMechAttacks += DisplayAttack;
            _battleController.OnMechWasAttacked += DisplayGetHit;
            _battleController.OnPartDestroyed += ShowDestroyedPart;
        }

        private void ShowDestroyedPart(Mech mech, MechPart part)
        {
            if (mech.IsPlayer)
            {
                _playerView.DestroyPart(part);
            } else
            {
                _enemyView.DestroyPart(part);
            }
        }

        private void AssignMech(Mech mech)
        {
            if (mech.IsPlayer)
            {
                _playerView.DisplayMech(mech);
            } else
            {
                _enemyView.DisplayMech(mech);
            }
        }

        private void DisplayGetHit(Mech mech, MechPart _, int damage)
        {
            if (mech.IsPlayer)
            {
                _playerView.GetHit(damage);
            }
            else
            {
                _enemyView.GetHit(damage);
            }
        }

        private void DisplayAttack(Mech mech)
        {
            if (mech.IsPlayer)
            {
                _playerView.Attack();
            } else
            {
                _enemyView.Attack();
            }
        }
    }
}
