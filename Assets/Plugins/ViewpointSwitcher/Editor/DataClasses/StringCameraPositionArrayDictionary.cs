using System.Collections.Generic;
using SerializableDictionary;

namespace ViewpointSwitcher.Editor.DataClasses
{
    [System.Serializable]
    public class StringCameraPositionArrayDictionary: SerializableDictionary<string, List<CameraPosition>, CameraPositionValueStorage>
    {
        
    }
}