using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechJam
{
    [CreateAssetMenu(fileName ="MechPart_", menuName = "Data/Mech Part")]
    public class MechPartData : ScriptableObject
    {
        [Header("Stats")]
        public PartType PartType;
        public Element Element;
        public int Tier;
        //TODO: Would like to have overheating as another balance valve
        [Header("Setup")]
        public string Name;
        public string Id;
        public Sprite Image;
        public GameObject Prefab;
    }

    public struct MechPart
    {
        public MechPart(MechPartData partData)
        {
            data = partData;
            var partValues = PartInfoHelper.GetValues(data.Tier, data.Element, data.PartType);
            MaxDurability = partValues.Durability;
            Durability = partValues.Durability;
            Attack = partValues.Attack;
            Defense = partValues.Defense;

        }
        public int MaxDurability { get; private set; }
        public int Durability { get; set; } 
        public int Attack { get; private set; }
        public int Defense { get; private set; }

        public MechPartData data { get; private set; }
    }
}
