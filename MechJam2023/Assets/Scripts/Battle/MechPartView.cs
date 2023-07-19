
using DG.Tweening;
using UnityEngine;

namespace MechJam
{
    public class MechPartView : MonoBehaviour
    {
        [SerializeField] private Transform _pivotPoint;
        public MechPart Part { get; set; }
        public Transform PivotPoint => _pivotPoint;

        private SpriteRenderer _sprite;
        public SpriteRenderer Sprite
        {
            get
            {
                if (_sprite == null) _sprite = GetComponentInChildren<SpriteRenderer>();
                return _sprite;
            }
        }

        internal void ShowDestroyed()
        {
            Sprite.DOFade(0.1f, 1f);
        }
    }
}
