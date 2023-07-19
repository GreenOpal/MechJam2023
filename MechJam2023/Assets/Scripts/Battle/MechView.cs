
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Parts = MechJam.Mech.AttackPart;


namespace MechJam
{ 
    public class MechView : MonoBehaviour
    {
        [SerializeField] private MechVfxController mechVfxController;

        [SerializeField] private Transform _headLocation;
        [SerializeField] private Transform _leftArmLocation;
        [SerializeField] private Transform _rightArmLocation;
        [SerializeField] private Transform _leftLegLocation;
        [SerializeField] private Transform _rightLegLocation;

        private List<MechPartView> _activeMechParts = new List<MechPartView>();
        public string MechName;

        private Mech mech;

        public void DisplayMech(Mech mech)
        {
            this.mech = mech;
            foreach (var currentPart in _activeMechParts)
            {
                Destroy(currentPart.gameObject);
            }
            _activeMechParts.Clear();
            MechName = mech.Name;
            PositionMechPart(Parts.Head, _headLocation);
            PositionMechPart(Parts.LeftArm, _leftArmLocation);
            PositionMechPart(Parts.RightArm, _rightArmLocation);
            PositionMechPart(Parts.LeftLeg, _leftLegLocation);
            PositionMechPart(Parts.RightLeg, _rightLegLocation);
        }

        public void DestroyPart(MechPart deadPart)
        {
            var deadPartView = _activeMechParts.FirstOrDefault((part) => part.Part.data.Id == deadPart.data.Id);
            if (deadPartView != default)
            {
                deadPartView.ShowDestroyed();
            }
        }

        private void PositionMechPart(Parts part, Transform location)
        {
            var mechPart = mech.PartMap[part];
            var newPartView = Instantiate(mechPart.data.Prefab, location);
            newPartView.transform.Translate(-newPartView.PivotPoint.localPosition);
            newPartView.Part = mechPart;
            _activeMechParts.Add(newPartView);
            
            switch (part)
            {
                case Parts.LeftArm:
                    newPartView.Sprite.sortingOrder = 3;
                    break;
                case Parts.LeftLeg:
                    newPartView.Sprite.sortingOrder = 2;
                    break;
                case Parts.Head:
                    newPartView.Sprite.sortingOrder = 1;
                    break;
                case Parts.RightLeg:
                    newPartView.Sprite.sortingOrder = -1;
                    break;
                case Parts.RightArm:
                    newPartView.Sprite.sortingOrder = -2;
                    break;
            }
        }

        public void Attack()
        {
            mechVfxController.ShowAttackVfx();
        }
        public void GetHit(int damage)
        {
            mechVfxController.ShowHitVfx(damage);
        }
    }
}
