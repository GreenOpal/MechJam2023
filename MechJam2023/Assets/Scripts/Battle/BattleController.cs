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
        public Action<Mech> OnMechAssigned;

        public Action<Mech> OnMechAttacks;
        public Action<Mech,MechPart,int> OnMechWasAttacked;
        PartCollection[] orderedParts;
        #endregion

        #region Methods

        protected override void Awake()
        {
            base.Awake();
            PartInfoHelper.Setup(Config.MechPartValues);

            orderedParts = new PartCollection[] {Config.Heads, Config.LeftArms, Config.RightArms,
                                                     Config.LeftLegs, Config.RightLegs};

            PlayerMech = new Mech();
            var playerMechParts = GetMechOfStrength(Config.DifficultySettings.GetDifficulty(Config.DifficultySettings.SelectedDifficulty, true));
            PlayerMech.Setup(this, true, "PlayerMech", playerMechParts);
            OnMechAssigned.Invoke(PlayerMech);
            EnemyMech = new Mech();
            var enemyMechParts = GetMechOfStrength(Config.DifficultySettings.GetDifficulty(Config.DifficultySettings.SelectedDifficulty, false));
            EnemyMech.Setup(this, false, "EnemyMech",enemyMechParts);
            OnMechAssigned.Invoke(EnemyMech);
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

        (MechPart, MechPart, MechPart, MechPart, MechPart) GetMechOfStrength(Vector3 strength)
        {
            List<MechPart> parts = new List<MechPart>();
            foreach (var collection in orderedParts)
            {
                float value = UnityEngine.Random.Range(0, 100);
                int tier = 1;
                if (value > strength.x + strength.y)
                {
                    tier = 3;
                } else if (value > strength.x)
                {
                    tier = 2;
                }
                parts.Add(collection.GetPart(ChosenTier: tier));
            }
            return (parts[0], parts[1], parts[2], parts[3], parts[4]);
        }

        #endregion

    }

    
}
