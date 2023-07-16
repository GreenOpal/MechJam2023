using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


namespace MechJam
{
    public class BattleController : GameControllerBase
    {
        #region Fields
        public Mech PlayerMech;
        public Mech EnemyMech;
        #endregion

        #region Actions
        public Action<Mech> OnPlayerMechAssigned;
        public Action<Mech> OnEnemyMechAssigned;

        public Action<Mech> OnMechAttacks;
        public Action<Mech,MechPart> OnMechWasAttacked;
        #endregion

        #region Methods

        protected override void Awake()
        {
            base.Awake();
            PlayerMech = new Mech();
            PlayerMech.Setup(this, true, "PlayerMech",GetRandomMech());
            OnPlayerMechAssigned?.Invoke(PlayerMech);
            EnemyMech = new Mech();
            EnemyMech.Setup(this, false, "EnemyMech",GetRandomMech());
            OnEnemyMechAssigned?.Invoke(EnemyMech);
        }
        (MechPart, MechPart, MechPart, MechPart, MechPart ) GetRandomMech()
        {
            return (Config.Heads.GetRandomPart(),
                Config.LeftArms.GetRandomPart(),
                Config.RightArms.GetRandomPart(),
                Config.LeftLegs.GetRandomPart(),
                Config.RightLegs.GetRandomPart()
                );
        }

        #endregion

    }

    
}
