using SerializableDictionary.Editor;
using SerializableDictionary.Editor.Plugins.ViewpointSwitcher.SerializableDictionary.Editor;
using UnityEditor;

namespace ViewpointSwitcher.Editor.DataClasses
{
    [CustomPropertyDrawer(typeof(CameraPositionValueStorage))]
    public class AnySerializableDictionaryStorageDrawer : SerializableDictionaryStoragePropertyDrawer
    {
        
    }
}