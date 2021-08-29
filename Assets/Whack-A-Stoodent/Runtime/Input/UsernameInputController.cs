using System;
using TMPro;
using UnityEngine;
using WhackAStoodent.Events;
using WhackAStoodent.Helper;

namespace WhackAStoodent.Input
{
    public class UsernameInputController : MonoBehaviour
    {
        [SerializeField] private StringEvent usernameInputEvent;
        [SerializeField] private TMP_InputField usernameInputField;


        private void Awake()
        {
            usernameInputField.text = StorageUtility.LoadClientName();
        }

        public void HandleUsernameConfirmation()
        {
            var input_user_name = usernameInputField.text;
            if(IsUsernameValid(input_user_name))
                usernameInputEvent.Invoke(input_user_name);
            else
                Debug.LogWarning("Username not valid");
        }

        private bool IsUsernameValid(string inputUserName)
        {
            return !string.IsNullOrWhiteSpace(inputUserName);
        }
    }
}
