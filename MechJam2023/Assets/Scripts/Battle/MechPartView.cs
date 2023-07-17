using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechJam
{
    public class MechPartView : MonoBehaviour
    {
        [SerializeField] private Transform _pivotPoint;
        public Transform PivotPoint => _pivotPoint;
    }
}
