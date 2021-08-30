using UnityEngine;

namespace WhackAStoodent.UI
{
    public class UIScreenController : MonoBehaviour
    {
        [SerializeField] private Transform content;
        private bool isActive = false;

        private void Start()
        {
            UpdateContentActive();
        }

        [ContextMenu("Activate")]
        public void Activate()
        {
            isActive = true;
            UpdateContentActive();
        }
        [ContextMenu("Deactivate")]
        public void Deactivate()
        {
            isActive = false;
            UpdateContentActive();
        }

        private void UpdateContentActive()
        {
            Debug.Log($"Setting content active to {isActive}");
            content.gameObject.SetActive(isActive);
        }

    }
}