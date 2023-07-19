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
        private Sequence _CurrentSequence;

        [ContextMenu("Test Attack")]
        public void ShowAttackVfx()
        {
            //Initial Tween-based implementation
            if (_CurrentSequence != null) _CurrentSequence.Complete();

            _CurrentSequence = DOTween.Sequence();
            _CurrentSequence.Append(_TweenTarget.DOMove(_AttackDirection, 0.25f).SetRelative(true));
            _CurrentSequence.Append(_TweenTarget.DOMove(-_AttackDirection, 1f).SetRelative(true).SetEase(Ease.OutQuad));
            _CurrentSequence.Play();
        }

        [ContextMenu("Test Hit")]
        public void ShowHitVfx(int damage)
        {
            if (_CurrentSequence != null) _CurrentSequence.Complete();
            _CurrentSequence = DOTween.Sequence();
            int vibrato = Mathf.FloorToInt(Mathf.Lerp(3, 12, Mathf.InverseLerp(0, 50, damage)));
            _CurrentSequence.Append(_TweenTarget.DOShakePosition(1f,vibrato: vibrato));
        }
    }
}
