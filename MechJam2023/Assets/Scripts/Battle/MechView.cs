
using System.Collections.Generic;
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

        private List<GameObject> _activeMechParts = new List<GameObject>();
        public string MechName;

        private Mech mech;

        public void DisplayMech(Mech mech)
        {
            this.mech = mech;
            foreach (var currentPart in _activeMechParts)
            {
                Destroy(currentPart);
            }
            _activeMechParts.Clear();
            MechName = mech.Name;
            PositionMechPart(Parts.Head, _headLocation);
            PositionMechPart(Parts.LeftArm, _leftArmLocation);
            PositionMechPart(Parts.RightArm, _rightArmLocation);
            PositionMechPart(Parts.LeftLeg, _leftLegLocation);
            PositionMechPart(Parts.RightLeg, _rightLegLocation);
        }

        private void PositionMechPart(Parts part, Transform location)
        {
            var mechPart = mech.PartMap[part];
            var newPart = Instantiate(mechPart.data.Prefab, location);
            newPart.transform.Translate(-newPart.PivotPoint.localPosition);
            var spriteRenderer = newPart.GetComponentInChildren<SpriteRenderer>();
            
            switch (part)
            {
                case Parts.LeftArm:
                    spriteRenderer.sortingOrder = 3;
                    break;
                case Parts.LeftLeg:
                    spriteRenderer.sortingOrder = 2;
                    break;
                case Parts.Head:
                    spriteRenderer.sortingOrder = 1;
                    break;
                case Parts.RightLeg:
                    spriteRenderer.sortingOrder = -1;
                    break;
                case Parts.RightArm:
                    spriteRenderer.sortingOrder = -2;
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
