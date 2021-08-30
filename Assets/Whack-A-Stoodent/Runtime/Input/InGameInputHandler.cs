using UnityEngine;
using UnityEngine.Events;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Input
{
    public class InGameInputHandler : MonoBehaviour
    {
        [SerializeField] private Camera inGameCamera;

        [Header("Invoked Input Events")]
        [SerializeField] private Vector2Event hitterHitInputEvent;
        [SerializeField] private NoParameterEvent moleHidInputEvent;
        [SerializeField] private HoleIndexEvent moleLookedInputEvent;
        
        [Header("Listened Server Events")]
        [SerializeField] private HoleIndexEvent moleLookedEvent;
        [SerializeField] private NoParameterEvent moleHidEvent;

        private static bool _listeningForMoleInput;
        private EHoleIndex? _lastMoleLook = null;

        private void OnEnable()
        {
            moleLookedEvent.Subscribe(HandleMoleLooked);
            moleHidEvent.Subscribe(HandleMoleHid);
        }

        private void HandleMoleHid()
        {
            _lastMoleLook = null;
        }

        private void HandleMoleLooked(EHoleIndex holeIndex)
        {
            _lastMoleLook = holeIndex;
        }

        private void Update()
        {
            if (_listeningForMoleInput)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ProcessMoleClick();
                }
            }
            else
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ProcessHitClick();
                }
            }
        }

        private void ProcessMoleClick()
        {
            Vector3 position = UnityEngine.Input.mousePosition;
            Vector2 hit_position = inGameCamera.ScreenToViewportPoint(position);
            var hole_index = hit_position.x > .5 ? 
                (hit_position.y > .5 ? EHoleIndex.TopRight : EHoleIndex.BottomRight) 
                : (hit_position.y > .5 ? EHoleIndex.TopLeft : EHoleIndex.BottomLeft);
            if(hole_index == _lastMoleLook)
            {
                Debug.Log("Invoking Hid Looked Input");
                moleHidInputEvent.Invoke();
            }
            else if(hole_index != _lastMoleLook)
            {
                Debug.Log("Invoking Mole Looked Input");
                moleLookedInputEvent.Invoke(hole_index);
            }
        }

        private void ProcessHitClick()
        {
            Vector3 position = UnityEngine.Input.mousePosition;
            Vector2 hit_position = inGameCamera.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
            hitterHitInputEvent.Invoke(hit_position);
        }

        public static void SetPlayerGameRole(EGameRole gameRole)
        {
            _listeningForMoleInput = gameRole == EGameRole.Mole;
        }
    }
}