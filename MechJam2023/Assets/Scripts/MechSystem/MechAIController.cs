using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MechJam
{
    public class MechAIController : BattleConfigurableBehaviour
    {
        [SerializeField] private float enemyAttackDelay = 1f;
        private Mech enemyMech;
        private Mech playerMech;
        private bool preparingAttack;
        public override void Initialize(GameControllerBase controller)
        {
            base.Initialize(controller);
            _battleController.OnEnemyMechAssigned += (mech) => enemyMech = mech;
            _battleController.OnPlayerMechAssigned += (mech) => playerMech = mech;
            _battleController.OnMechAttacks += PrepareEnemyAttack;
        }

        private void PrepareEnemyAttack(Mech mech)
        {
            if (preparingAttack || mech.Name != enemyMech.Name) return;

            StartCoroutine(QueueEnemyAttack());
        }
        private IEnumerator QueueEnemyAttack()
        {
            preparingAttack = true;

            yield return new WaitForSeconds(enemyAttackDelay);
            enemyMech.DetermineAIAttack(playerMech);
        }


    }
}