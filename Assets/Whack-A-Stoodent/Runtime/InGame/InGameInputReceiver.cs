using System;
using UnityEngine;

namespace WhackAStoodent.InGame
{
    public class InGameInputReceiver : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _targetLayerMask;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ProcessClick();
            }
        }

        private void ProcessClick()
        {
            Vector3 position = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(position, Camera.MonoOrStereoscopicEye.Mono);
            
            //Debug.Log($"Ray from {ray.origin} in direction {ray.direction}");
            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f, _targetLayerMask, QueryTriggerInteraction.Collide))
            {
                Debug.Log($"hit target {hit.collider.gameObject.name} at {(Vector2)hit.point}");
            }
        }
    }
}