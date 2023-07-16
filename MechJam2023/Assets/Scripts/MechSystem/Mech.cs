
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
        public string Name { get; private set; }
        public bool IsPlayer { get; private set; }

        public Dictionary<AttackPart, MechPart> PartMap;
        public BattleController battleController;

        public void Setup (BattleController controller, bool isPlayer, string name, (MechPart, MechPart, MechPart, MechPart, MechPart) parts)
        {
            Setup(controller, isPlayer, name, parts.Item1, parts.Item2, parts.Item3, parts.Item4, parts.Item5);
        }
        public void Setup(BattleController controller, bool isPlayer, string name, MechPart head, MechPart leftArm, MechPart rightArm,
            MechPart leftLeg, MechPart rightLeg)
        {
            IsPlayer = isPlayer;
            battleController = controller;
            Name = name;
            PartMap = new Dictionary<AttackPart, MechPart> {
                { AttackPart.Head,head },
                { AttackPart.LeftArm,leftArm},
                { AttackPart.RightArm,rightArm },
                { AttackPart.LeftLeg,leftLeg },
                { AttackPart.RightLeg,rightLeg }
            };
            MechReport();
        }

        private void TestElements()
        {
            //Verification function to make sure elements are doing what they're supposed to!
            Debug.LogWarning($"   def: SH ST SM" +
                $"\natt SH: {ElementHelper.GetElementEffect(Element.Shooty, Element.Shooty)} / {ElementHelper.GetElementEffect(Element.Shooty, Element.Stabby)} / {ElementHelper.GetElementEffect(Element.Shooty, Element.Smashy)}" +
                $"\natt ST: {ElementHelper.GetElementEffect(Element.Stabby, Element.Shooty)} / {ElementHelper.GetElementEffect(Element.Stabby, Element.Stabby)} / {ElementHelper.GetElementEffect(Element.Stabby, Element.Smashy)}" +
                $"\natt SM: {ElementHelper.GetElementEffect(Element.Smashy, Element.Shooty)} / {ElementHelper.GetElementEffect(Element.Smashy, Element.Stabby)} / {ElementHelper.GetElementEffect(Element.Smashy, Element.Smashy)}");

            
        }

        public void Attack(MechPart weapon, Mech opponent, AttackPart target)
        {
            var attack = DetermineAttackValue(weapon);
            opponent.GetHit(attack, weapon, target);
            battleController.OnMechAttacks?.Invoke(this);
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
            PartMap[target] = targetPart;
            Debug.Log($"Part {targetPart.data.Name} ({targetPart.data.Element}) takes {damageTaken} damage!");
            CheckPartDurability(PartMap[target]);
            battleController.OnMechWasAttacked?.Invoke(this, weapon);
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
            var availableWeapons = PartMap.Values.Where((part) => part.Durability > 0 && part.data.PartType != PartType.Head).ToArray();
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
            return weapon.data.Attack;
        }
        private int DetermineDamageTaken(int baseDamage, MechPart weapon, MechPart target)
        {
            //Same caveats as above, keep it real basic to start, will need to ensure defense values are lower than dmg
            var damage = ElementHelper.GetElementEffect(weapon.data.Element, target.data.Element) * baseDamage;
            damage -= target.data.Defense;
            return Mathf.Max( Mathf.FloorToInt(damage), 1);
        }

        private void MechReport()
        {
            //Some simple debug info
            Debug.LogWarning($"Report on mech {Name}:" +
                $"\nHead: ({PartMap[AttackPart.Head].data.Element}) {PartMap[AttackPart.Head].Durability}/100 " +
                $"\nL_Arm: ({PartMap[AttackPart.LeftArm].data.Element}) {PartMap[AttackPart.LeftArm].Durability} " +
                $"R_Arm: ({PartMap[AttackPart.RightArm].data.Element}) {PartMap[AttackPart.RightArm].Durability}" +
                $"\nL_Leg: ({PartMap[AttackPart.LeftLeg].data.Element}) {PartMap[AttackPart.LeftLeg].Durability} " +
                $"R_Leg: ({PartMap[AttackPart.RightLeg].data.Element}) {PartMap[AttackPart.RightLeg].Durability}");
        }
    }

}
