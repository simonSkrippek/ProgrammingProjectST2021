using UnityEngine;
using UnityEngine.Events;

namespace WhackAStoodent.InGame
{
    public class InGameInputHandler : MonoBehaviour
    {
        [SerializeField] private Camera inGameCamera;
        [SerializeField] private bool listeningForMoleInput;

        [SerializeField] public UnityEvent<Vector2> hitterHit;
        
        private void Update()
        {
            if (listeningForMoleInput)
            {
                //TODO mole input
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ProcessClick();
                }
            }
        }

        private void ProcessClick()
        {
            Vector3 position = Input.mousePosition;
            Vector2 hit_position = inGameCamera.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
            hitterHit?.Invoke(hit_position);
        }
    }
}