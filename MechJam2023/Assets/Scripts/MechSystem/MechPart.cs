using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechJam
{
    [CreateAssetMenu(fileName ="MechPart_", menuName = "Data/Mech Part")]
    public class MechPart : ScriptableObject
    {
        [Header("Stats")]
        [Range(0, 100)] public int Durability = 100;
        [Range(0, 100)] public int Attack = 50;
        [Range(0, 100)] public int Defense = 50;
        public PartType PartType;
        public Element Element;
        //TODO: Would like to have overheating as another balance valve
        [Header("Setup")]
        public string Name;
        public string Id;
        public Sprite Image;
        public GameObject Prefab;
    }
}
