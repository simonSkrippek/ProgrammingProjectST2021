using System;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.InGame;
using Random = UnityEngine.Random;

namespace WhackAStoodent
{
    [RequireComponent(typeof(FaceController))]
    public class RandomFaceTrigger : MonoBehaviour
    {
        [SerializeField] private float maxTimeBetweenTriggers = 3f;
        [SerializeField, Range(0, 100)] private int hidePercentage = 30;

        private float _timeToNextChange;
        private FaceController _faceController;

        private void Awake()
        {
            _faceController = GetComponent<FaceController>();
        }

        private void Update()
        {
            _timeToNextChange -= Time.deltaTime;
            if (_timeToNextChange <= 0)
            {
                MakeChange();
                _timeToNextChange = Random.Range(0f, maxTimeBetweenTriggers);
            }
        }

        private void MakeChange()
        {
            if(_faceController.IsFaceShowing && Random.Range(0, 101) < hidePercentage)
            {
                _faceController.DespawnFace();
            }
            else
            {
                _faceController.SpawnFace((EHoleIndex) Random.Range(0, 4));
            }
        }
    }
}
