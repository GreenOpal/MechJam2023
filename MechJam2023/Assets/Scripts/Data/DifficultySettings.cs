using UnityEngine;

namespace MechJam
{
    [CreateAssetMenu(fileName ="DifficultySettings", menuName = "Data/Difficulty")]
    public class DifficultySettings : ScriptableObject
    {
        public int SelectedDifficulty; // 0,1,2: Easy,Medium,Hard

        public Vector3 Ratio_Player_Easy;
        public Vector3 Ratio_Player_Medium;
        public Vector3 Ratio_Player_Hard;
        public Vector3 Ratio_Enemy_Easy;
        public Vector3 Ratio_Enemy_Medium;
        public Vector3 Ratio_Enemy_Hard;

        public Vector3 GetDifficulty(int value, bool isPlayer)
        {
            switch((value, isPlayer))
            {
                case (0, true):
                    return Ratio_Player_Easy;
                case (1, true):
                    return Ratio_Player_Medium;
                case (2, true):
                    return Ratio_Player_Hard;
                case (0, false):
                    return Ratio_Enemy_Easy;
                case (1, false):
                    return Ratio_Enemy_Medium;
                case (2, false):
                    return Ratio_Enemy_Hard;
                default:
                    throw new System.Exception("eh?");
            }
        }
    }

}
