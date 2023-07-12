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
        }
    }
}
