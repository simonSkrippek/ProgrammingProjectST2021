using UnityEngine;

namespace ViewpointSwitcher.Editor.DataClasses
{
    public class ViewpointSwitcherStorage : ScriptableObject
    {
        [SerializeField] private StringCameraPositionArrayDictionary viewpointDict = new StringCameraPositionArrayDictionary();

        public StringCameraPositionArrayDictionary ViewpointDict => viewpointDict ?? (viewpointDict = new StringCameraPositionArrayDictionary());
    }
}