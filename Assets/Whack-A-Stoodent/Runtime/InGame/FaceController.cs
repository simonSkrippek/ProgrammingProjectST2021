using DG.Tweening;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;

namespace WhackAStoodent.InGame
{
    public class FaceController : MonoBehaviour
    {
        [SerializeField] private FaceFactory faceFactory;
        [SerializeField] private float faceMovementDistance = 3;
        [SerializeField] private float faceMovementDuration = 3;
        [SerializeField] private Transform[] faceParentsByHoleIndex;
        [SerializeField] private Transform[] faceEndPositionsByHoleIndex;

        private GameObject _lastFace;
        private Tween _spawnTween;

        private void DespawnFace()
        {
            if(!_lastFace) return;
            //tween lastFace out and destroy after 
            var face_to_tween = _lastFace;
            _spawnTween?.Kill();
            _spawnTween = null;
            _lastFace = null;
            face_to_tween.transform.DOMoveY(face_to_tween.transform.position.y - faceMovementDistance, faceMovementDuration)
                .SetEase(Ease.InOutBack)
                .onComplete += () => { Destroy(face_to_tween); };
        }
        private void SpawnFace(EHoleIndex holeIndex)
        {
            if(_lastFace) DespawnFace();
            
            var new_face = faceFactory.GetNewFace(holeIndex, faceParentsByHoleIndex[holeIndex.Index()]);
            new_face.transform.position = GetFaceStartPosition();
            //tween face in, save as lastFace
            _lastFace = new_face;
            _spawnTween = new_face.transform.DOMoveY(faceEndPositionsByHoleIndex[holeIndex.Index()].position.y, faceMovementDuration)
                .SetEase(Ease.InOutBack);

            Vector3 GetFaceStartPosition()
            {
                var ret = faceEndPositionsByHoleIndex[holeIndex.Index()].position;
                ret.y -= faceMovementDistance;
                return ret;
            }
        }
        
        
        //test
        [ContextMenu("SpawnFace_TopLeft")]
        private void SpawnFace0()
        {
            SpawnFace(EHoleIndex.TopLeft);
        }
        [ContextMenu("SpawnFace_TopRight")]
        private void SpawnFace1()
        {
            SpawnFace(EHoleIndex.TopRight);
        }
        [ContextMenu("SpawnFace_BottomRight")]
        private void SpawnFace2()
        {
            SpawnFace(EHoleIndex.BottomRight);
        }
        [ContextMenu("SpawnFace_BottomLeft")]
        private void SpawnFace3()
        {
            SpawnFace(EHoleIndex.BottomLeft);
        }
        [ContextMenu("DespawnFace")]
        private void DespawnFaceMenu() => DespawnFace();
        
        [ContextMenu("SpawnFace_TopLeft", true)]
        [ContextMenu("SpawnFace_TopRight", true)]
        [ContextMenu("SpawnFace_BottomRight", true)]
        [ContextMenu("SpawnFace_BottomLeft", true)]
        [ContextMenu("DespawnFace", true)]
        private bool CanSpawnFace()
        {
            return Application.isPlaying;
        }
    }
}