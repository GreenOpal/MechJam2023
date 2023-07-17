
using System.Linq;
using UnityEngine;

namespace MechJam {
    [CreateAssetMenu(fileName = "Parts_", menuName = "Data/Mech Part Collection")]

    public class PartCollection : ScriptableObject
    {
        public MechPartData[] AllParts;
        public MechPart GetRandomPart()
        {
            return new MechPart(AllParts[Random.Range(0, AllParts.Length)]);
        }
        public MechPart GetPart(int ChosenTier = 0, bool ChooseElement = false, Element ChosenElement = default)
        {
            System.Collections.Generic.IEnumerable<MechPartData> validParts = AllParts;
            if (ChosenTier > 0)
            {
                validParts = validParts.Where((part) => part.Tier == ChosenTier);
            }
            if (ChooseElement)
            {
                validParts = validParts.Where((part) => part.Element == ChosenElement);
            }
            var partsArray = validParts.ToArray();
            var part = partsArray[Random.Range(0, partsArray.Length)];
            Debug.LogWarning($"Get part {part}: tier {part.Tier}, element {part.Element}");

            return new MechPart(partsArray[Random.Range(0, partsArray.Length)]);
        }
    }
}
