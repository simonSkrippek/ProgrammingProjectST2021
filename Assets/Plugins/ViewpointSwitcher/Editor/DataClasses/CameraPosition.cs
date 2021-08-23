using UnityEngine;

namespace ViewpointSwitcher.Editor.DataClasses
{
    [System.Serializable]
    public struct CameraPosition
    {
        public Vector3 pivot;
        public Quaternion rotation;
        public float size;
        public string name;

        public CameraPosition(string name, Vector3 pivot, Quaternion rotation, float size)
        {
            this.name = name;
            this.pivot = pivot;
            this.rotation = rotation;
            this.size = size;
        }

        public override bool Equals(System.Object other)
        {
            return (other is CameraPosition other_pos) && other_pos.name.Equals(name);
        }

        public bool Equals(CameraPosition other)
        {
            return name == other.name;
        }

        public override int GetHashCode()
        {
            return (name != null ? name.GetHashCode() : 0);
        }
    }
}