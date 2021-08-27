using System.Collections.Generic;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.InGame
{
    [CreateAssetMenu(fileName = "FaceFactory", menuName = "Custom/FaceFactory")]
    public class FaceFactory : ScriptableObject
    {
        [SerializeField] private GameObject facePrefab;
        [SerializeField] private Sprite[] faceSprites;
        [SerializeField] private string[] faceSortingLayersByHoleIndex;

        private int lastFaceIndex;

        public GameObject GetNewFace(EHoleIndex holeIndex, Transform parent)
        {
            var new_face = Instantiate(facePrefab, parent);
            var new_face_renderer = new_face.GetComponent<SpriteRenderer>();
            lastFaceIndex = GetNewFaceIndex();
            new_face_renderer.sprite = faceSprites[lastFaceIndex];
            new_face_renderer.sortingLayerName = faceSortingLayersByHoleIndex[holeIndex.Index()];
            return new_face;
        }

        private int GetNewFaceIndex()
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < faceSprites.Length; i++)
            {
                indices.Add(i);
            }

            indices.Remove(lastFaceIndex);
            return indices[Random.Range(0, indices.Count)];
        }
    }
}
