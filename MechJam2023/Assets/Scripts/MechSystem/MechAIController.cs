using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MechJam
{
    public class MechAIController : BattleConfigurableBehaviour
    {
        [SerializeField] private float enemyAttackDelay = 3f;
        private bool preparingAttack;
        public override void Initialize(GameControllerBase controller)
        {
            base.Initialize(controller);
            _battleController.OnMechAttacks += PrepareEnemyAttack;
        }

        private void PrepareEnemyAttack(Mech mech)
        {
            if (preparingAttack || !mech.IsPlayer ) return;

            StartCoroutine(QueueEnemyAttack(mech));
        }
        private IEnumerator QueueEnemyAttack(Mech mech)
        {
            preparingAttack = true;

            yield return new WaitForSeconds(enemyAttackDelay);
            mech.DetermineAIAttack(_battleController.PlayerMech);
            preparingAttack = false;
        }


    }
}