using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace WhackAStoodent.InGame
{
    public class HammerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform hammerRoot;
        [SerializeField] private Transform hammerAppearance;
        [SerializeField] private SpriteRenderer hammerRenderer;
        [SerializeField] private Transform screenMiddleMarker;

        [SerializeField] private Transform debugHitPosition;
        
        [Header("Settings")]
        [SerializeField] private float prepAngle;
        [SerializeField] private float restingAngle;
        [SerializeField] private float hittingAngle;
        [SerializeField] private float prepDuration;
        [SerializeField] private float hitDuration;
        [SerializeField] private float recoveryDuration;

        public void Hit(Vector2 position)
        {
            hammerRoot.rotation = Quaternion.Euler(new Vector3(0, position.x < screenMiddleMarker.position.x ? 180 : 0, 0));
            
            hammerRoot.DOMove(position, prepDuration)
                .SetEase(Ease.OutCirc);
            
            hammerAppearance.DOLocalRotate(new Vector3(0, 0, prepAngle), prepDuration)
                .SetEase(Ease.OutQuad)
                .onComplete += () =>
            {
                hammerAppearance.DOLocalRotate(new Vector3(0, 0, hittingAngle), hitDuration)
                        .SetEase(Ease.Linear)
                        .onComplete +=
                    () =>
                    {
                        hammerAppearance.DOLocalRotate(new Vector3(0, 0, restingAngle), recoveryDuration)
                            .SetEase(Ease.InOutSine);
                    };
            };
        }

        [ContextMenu("Hit")]
        private void HitDebugPosition()
        {
            Hit(debugHitPosition.position);
        }
        [ContextMenu("Hit", true)]
        private bool CanHit()
        {
            return Application.isPlaying;
        }
    }
}