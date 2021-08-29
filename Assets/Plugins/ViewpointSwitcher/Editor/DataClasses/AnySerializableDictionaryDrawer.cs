using SerializableDictionary.Editor.Plugins.ViewpointSwitcher.SerializableDictionary.Editor;
using UnityEditor;

namespace ViewpointSwitcher.Editor.DataClasses
{
    [CustomPropertyDrawer(typeof(StringCameraPositionArrayDictionary))]
    public class AnySerializableDictionaryDrawer : SerializableDictionaryPropertyDrawer
    {
        
    }
}