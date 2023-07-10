
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
    }
}
