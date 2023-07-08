
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace MechJam {
    public class Mech
    {
        public enum AttackPart
        {
            //Gonna be very cheeky and use this as a int lookup with the UI, what could possibly go wrong
            Head, //Can be a target but not a weapon in current plan
            LeftArm,
            RightArm,
            LeftLeg,
            RightLeg,
        }
        public string Name;
        public MechPart Head;
        public MechPart LeftArm;
        public MechPart RightArm;
        public MechPart LeftLeg;
        public MechPart RightLeg;

        public Dictionary<AttackPart, MechPart> PartMap;

        public void Setup (string name, (MechPart, MechPart, MechPart, MechPart, MechPart) parts)
        {
            Setup(name, parts.Item1, parts.Item2, parts.Item3, parts.Item4, parts.Item5);
        }
        public void Setup(string name, MechPart head, MechPart leftArm, MechPart rightArm, 
            MechPart leftLeg, MechPart rightLeg)
        {
            Name = name;
            Head = head;
            LeftArm = leftArm;
            RightArm = rightArm;
            LeftLeg = leftLeg;
            RightLeg = rightLeg;
            PartMap = new Dictionary<AttackPart, MechPart> {
                { AttackPart.Head,head },
                { AttackPart.LeftArm,leftArm},
                { AttackPart.RightArm,rightArm },
                { AttackPart.LeftLeg,leftLeg },
                { AttackPart.RightLeg,rightLeg }
            };
            MechReport();
        }

        public void Attack(MechPart weapon, Mech opponent, AttackPart target)
        {
            var attack = DetermineAttackValue(weapon);
            opponent.GetHit(attack, weapon, target);
        }
        public void Attack(AttackPart weapon, Mech opponent, AttackPart target)
        {
            Attack(PartMap[weapon], opponent, target);
        }

        public void GetHit(int attack, MechPart weapon, AttackPart target)
        {
            int damageTaken = DetermineDamageTaken(attack, weapon, PartMap[target]);
            var targetPart = PartMap[target];
            targetPart.Durability -= damageTaken;
            Debug.Log($"Part {targetPart.Name} ({targetPart.Element}) takes {damageTaken} damage!");
            CheckPartDurability(PartMap[target]);
        }

        private void CheckPartDurability(MechPart mechPart)
        {
            if (mechPart.Durability <= 0)
            {
                mechPart.Durability = 0;
            }
            MechReport();
        }

        public void DetermineAIAttack(Mech opponent)
        {
            var availableWeapons = PartMap.Values.Where((part) => part.Durability > 0 && part.PartType != PartType.Head).ToArray();
            var weapon = availableWeapons[Random.Range(0, availableWeapons.Length)];

            var availableTargets = opponent.PartMap.Where((part) => part.Value.Durability > 0).ToArray();
            var target = availableTargets[Random.Range(0, availableTargets.Length)];
            //To be refined
            Attack(weapon, opponent, target.Key);
        }

        //Mech Calculation stuff - could be moved to some helper/battle controller class
        private int DetermineAttackValue(MechPart weapon)
        {
            //Lots of stuff to figure out here - head variation? Random pertubation? Does  tier factor into this beyond base stats? But start simple
            return weapon.Attack;
        }
        private int DetermineDamageTaken(int baseDamage, MechPart weapon, MechPart target)
        {
            //Same caveats as above, keep it real basic to start, will need to ensure defense values are lower than dmg
            var damage = ElementHelper.GetElementEffect(weapon.Element, target.Element) * baseDamage;
            damage -= target.Defense;
            return Mathf.Max( Mathf.FloorToInt(damage), 1);
        }

        private void MechReport()
        {
            //Some simple debug info
            Debug.LogWarning($"Report on mech {Name}:" +
                $"\nHead: ({Head.Element}) {Head.Durability}/100 " +
                $"\nL_Arm: ({LeftArm.Element}) {LeftArm.Durability} R_Arm: ({RightArm.Element}) {RightArm.Durability}" +
                $"\nL_Leg: ({LeftLeg.Element}) {LeftLeg.Durability} R_Leg: ({RightLeg.Element}) {RightLeg.Durability}");
        }
    }

}
