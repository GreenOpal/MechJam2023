using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechJam
{
    public class ElementHelper
    {
        private const float multiplier = 1.2f;
        public static float GetElementEffect(Element attacker, Element defense)
        {
            switch (((int)attacker - (int)defense) % 2)
            {
                case 0:
                    return 1;
                case 1:
                    return multiplier;
                case 2:
                    return 1f / multiplier;
                default:
                    Debug.LogError("Uh oh!");
                    return 0;
            }            
        }
    }
}
