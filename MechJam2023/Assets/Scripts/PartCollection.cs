
using UnityEngine;

namespace MechJam {
    [CreateAssetMenu(fileName = "Parts_", menuName = "Data/Mech Part Collection")]

    public class PartCollection : ScriptableObject
    {
        public MechPart[] AllParts;
        public MechPart GetRandomPart()
        {
            return AllParts[Random.Range(0, AllParts.Length)];
        }
    }
}
