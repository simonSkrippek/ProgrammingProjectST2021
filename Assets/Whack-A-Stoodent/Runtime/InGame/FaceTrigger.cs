﻿using System.Collections;
using UnityEngine;
using WhackAStoodent.Client.Networking.Messages;
using WhackAStoodent.Events;

namespace WhackAStoodent.InGame
{
    public class FaceTrigger : MonoBehaviour
    {
        [SerializeField] private HoleIndexEvent moleLooked;
        [SerializeField] private NoParameterEvent moleHid;
        
        private FaceController _faceController;

        private void Awake()
        {
            _faceController = GetComponent<FaceController>();
            var coroutine = spawnCoroutine();
            StartCoroutine(coroutine);
        }

        private IEnumerator spawnCoroutine()
        {
            for (int i = 0; i < 10; i++)
            {
                _faceController.SpawnFace((EHoleIndex)Random.Range(0,4));
                yield return new WaitForSeconds(.5f);
            }
        }
        
        private void OnEnable()
        {
            moleLooked.Subscribe(HandleMoleLooked);
            moleHid.Subscribe(HandleMoleHid);
        }
        private void OnDisable()
        {
            moleLooked.Unsubscribe(HandleMoleLooked);
            moleHid.Unsubscribe(HandleMoleHid);
        }

        private void HandleMoleLooked(EHoleIndex holeIndex)
        {
            _faceController.SpawnFace(holeIndex);
        }
        private void HandleMoleHid()
        {
            _faceController.DespawnFace();
        }
        
        [ContextMenu("SpawnFace_TopLeft")]
        private void SpawnFace0()
        {
            HandleMoleLooked(EHoleIndex.TopLeft);
        }
        [ContextMenu("SpawnFace_TopRight")]
        private void SpawnFace1()
        {
            HandleMoleLooked(EHoleIndex.TopRight);
        }
        [ContextMenu("SpawnFace_BottomRight")]
        private void SpawnFace2()
        {
            HandleMoleLooked(EHoleIndex.BottomRight);
        }
        [ContextMenu("SpawnFace_BottomLeft")]
        private void SpawnFace3()
        {
            HandleMoleLooked(EHoleIndex.BottomLeft);
        }
        [ContextMenu("DespawnFace")]
        private void DespawnFaceMenu() => HandleMoleHid();
        
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