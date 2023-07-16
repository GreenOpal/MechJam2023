using UnityEngine;


namespace MechJam
{
    [CreateAssetMenu(fileName ="GameConfig", menuName = "GameStuff/Data")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float _PlayerSpeed;
        //Add Variables we want to expose/edit here

        public MechPartValues MechPartValues;

        public PartCollection Heads;
        public PartCollection LeftArms;
        public PartCollection RightArms;
        public PartCollection LeftLegs;
        public PartCollection RightLegs;
    }
}