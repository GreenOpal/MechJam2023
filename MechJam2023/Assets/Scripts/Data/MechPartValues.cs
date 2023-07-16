using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MechJam
{
    [CreateAssetMenu(fileName = "PartValues", menuName = "Data/Mech Part Values")]
    public class MechPartValues : ScriptableObject
    {
        [SerializeField] public PartValues_Tier[] Values;
        
        [System.Serializable]
        public struct PartValues_Tier
        {
            public int Tier;
            public PartValues_Element[] Elements;
        
        }
        [System.Serializable]
        public struct PartValues_Element
        {
            public Element Element;
            public PartValues_MechPart[] Parts;
        }
        [System.Serializable]
        public struct PartValues_MechPart
        {
            public PartType Part;

            public PartValues Values;
        }
        [System.Serializable]
        public struct PartValues 
        {
            public int Durability;
            public int Attack;
            public int Defense;
        }

    }
}
