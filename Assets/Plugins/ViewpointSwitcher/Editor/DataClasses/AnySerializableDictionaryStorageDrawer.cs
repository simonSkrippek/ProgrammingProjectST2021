using SerializableDictionary.Editor;
using UnityEditor;

namespace ViewpointSwitcher.Editor.DataClasses
{
    [CustomPropertyDrawer(typeof(CameraPositionValueStorage))]
    public class AnySerializableDictionaryStorageDrawer : SerializableDictionaryStoragePropertyDrawer
    {
        
    }
}