using TMPro;
using UnityEngine;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Input
{
    public class SessionCodeInputController : MonoBehaviour
    {
        [SerializeField] private StringEvent sessionCodeInputEvent;
        [SerializeField] private TMP_InputField sessionCodeInputField;


        private void OnEnable()
        {
            sessionCodeInputField.text = "";
        }

        public void HandleSessionCodeConfirmation()
        {
            var input_session_code = sessionCodeInputField.text;
            if(IsUsernameValid(input_session_code))
                sessionCodeInputEvent.Invoke(input_session_code);
            else
            {
                Debug.LogWarning("Session Code not valid");
                sessionCodeInputField.text = "";
            }
        }

        private bool IsUsernameValid(string inputSessionCode)
        {
            return !string.IsNullOrWhiteSpace(inputSessionCode) && inputSessionCode.Length == 6;
        }
    }
}
