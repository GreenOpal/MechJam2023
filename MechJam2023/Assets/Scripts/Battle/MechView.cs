using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parts = MechJam.Mech.AttackPart;


namespace MechJam
{ 
    public class MechView : MonoBehaviour
    {
        [SerializeField] private Transform _headLocation;
        [SerializeField] private Transform _leftArmLocation;
        [SerializeField] private Transform _rightArmLocation;
        [SerializeField] private Transform _leftLegLocation;
        [SerializeField] private Transform _rightLegLocation;

        private List<GameObject> _activeMechParts = new List<GameObject>();

        private Mech mech;

        public void DisplayMech(Mech mech)
        {
            this.mech = mech;
            foreach (var currentPart in _activeMechParts)
            {
                Destroy(currentPart);
            }
            _activeMechParts.Clear();
            PositionMechPart(mech.PartMap[Parts.Head], _headLocation);
            PositionMechPart(mech.PartMap[Parts.LeftArm], _leftArmLocation);
            PositionMechPart(mech.PartMap[Parts.RightArm], _rightArmLocation);
            PositionMechPart(mech.PartMap[Parts.LeftLeg], _leftLegLocation);
            PositionMechPart(mech.PartMap[Parts.RightLeg], _rightLegLocation);
        }

        private void PositionMechPart(MechPart part, Transform location)
        {
            var newPart = Instantiate(part.data.Prefab, location);
        }
    }
}
