using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MechJam
{
    public class MechVfxController : MonoBehaviour
    {
        [SerializeField] private Transform _TweenTarget;
        [SerializeField] private Vector3 _AttackDirection;

        [ContextMenu("Test Attack")]
        public void ShowAttackVfx()
        {
            //Initial Tween-based implementation
            var AttackSequence = DOTween.Sequence();
            AttackSequence.Append(_TweenTarget.DOMove(_AttackDirection, 0.25f).SetRelative(true));
            AttackSequence.Append(_TweenTarget.DOMove(-_AttackDirection, 1f).SetRelative(true).SetEase(Ease.OutQuad));
            AttackSequence.Play();
        }

        [ContextMenu("Test Hit")]
        public void ShowHitVfx()
        {
            _TweenTarget.DOShakePosition(1f,vibrato:6);
        }
    }
}
