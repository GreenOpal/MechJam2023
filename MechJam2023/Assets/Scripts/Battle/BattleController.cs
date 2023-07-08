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

        protected override void Awake()
        {
            base.Awake();
            PlayerMech = new Mech();
            PlayerMech.Setup("PlayerMech",GetRandomMech());
            EnemyMech = new Mech();
            EnemyMech.Setup("EnemyMech",GetRandomMech());
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
