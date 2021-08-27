using System;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;

namespace WhackAStoodent.InGame
{
    [RequireComponent(typeof(HammerController))]
    public class HammerTrigger : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private Vector2HoleIndexEvent hitSuccessfulEvent; 
        [SerializeField] private Vector2Event hitFailedEvent;

        [SerializeField] private Transform debugHitPosition;

        private HammerController _hammerController;

        private void Awake()
        {
            _hammerController = GetComponent<HammerController>();
        }
        private void OnEnable()
        {
            hitSuccessfulEvent.Subscribe(HandleSuccessfulHit);
            hitFailedEvent.Subscribe(HandleFailedHit);
        }
        private void OnDisable()
        {
            hitSuccessfulEvent.Unsubscribe(HandleSuccessfulHit);
            hitFailedEvent.Unsubscribe(HandleFailedHit);
        }

        private void HandleSuccessfulHit(Vector2 hitPosition, EHoleIndex hitHoleIndex)
        {
            HandleHit(hitPosition);
        }
        private void HandleFailedHit(Vector2 hitPosition)
        {
            HandleHit(hitPosition);
        }
        private void HandleHit(Vector2 hitPosition)
        {
            _hammerController.Hit(hitPosition);
        }
        
        

        [ContextMenu("Hit")]
        private void HitDebugPosition()
        {
            HandleHit(debugHitPosition.position);
        }
        [ContextMenu("Hit", true)]
        private bool CanHit()
        {
            return Application.isPlaying;
        }
    }
}