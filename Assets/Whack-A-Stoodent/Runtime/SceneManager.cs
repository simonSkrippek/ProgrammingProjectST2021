using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WhackAStoodent.Runtime
{
    [DefaultExecutionOrder(-5)]
    public class SceneManager : MonoBehaviour
    {
        private Dictionary<int, Scene> _loadedScenes = new Dictionary<int, Scene>();
        private Dictionary<int, AsyncOperation> _asyncSceneLoads = new Dictionary<int, AsyncOperation>();
        private Dictionary<int, AsyncOperation> _asyncSceneUnloads = new Dictionary<int, AsyncOperation>();
        public bool IsAsyncLoading => _asyncSceneLoads.Count > 0;
        private bool IsSceneLoadedOrLoading(int buildIndex) => _loadedScenes.ContainsKey(buildIndex) || _asyncSceneLoads.ContainsKey(buildIndex);
        private bool IsSceneLoadedAndNotUnloading(int buildIndex) => _loadedScenes.ContainsKey(buildIndex) && !_asyncSceneUnloads.ContainsKey(buildIndex);

        private void Awake()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoadedHandler;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloadedHandler;
        }

        private void OnSceneLoadedHandler(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (!_loadedScenes.ContainsKey(scene.buildIndex)) _loadedScenes.Add(scene.buildIndex, scene);
            else _loadedScenes[scene.buildIndex] = scene;
        }
        private void OnSceneUnloadedHandler(Scene scene)
        {
            _loadedScenes.Remove(scene.buildIndex);
        }

        public int GetActiveSceneIndex()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        }
        public void LoadScene(int buildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex, loadSceneMode);
        }
        public void LoadSceneAsync(int buildIndex, [NotNull] Action<AsyncOperation> onCompleteFunc, LoadSceneMode loadSceneMode)
        {
            if (!IsSceneLoadedOrLoading(buildIndex))
            {
                AsyncOperation async_scene_load = CreateAsyncSceneLoad(buildIndex, loadSceneMode);
                async_scene_load.completed += onCompleteFunc;
            }
        }
        public void LoadSceneAsync(int buildIndex, LoadSceneMode loadSceneMode, float time = 0)
        {
            if (!IsSceneLoadedOrLoading(buildIndex))
            {
                CreateAsyncSceneLoad(buildIndex, loadSceneMode);
                if (time > 0) Invoke(nameof(AllowAsyncSceneTransition), time);
                else AllowAsyncSceneTransition(buildIndex);
            }
        }
        public void AllowAsyncSceneTransition(int sceneBuildIndex)
        {
            if (_asyncSceneLoads.TryGetValue(sceneBuildIndex, out AsyncOperation scene_load))
                scene_load.allowSceneActivation = true;
        }
        public float GetAsyncLoadProcess(int[] sceneIndices = null)
        {
            if (sceneIndices == null) return CalculateCurrentAsyncLoadProgress();
            else return CalculateCurrentAsyncLoadProgress(sceneIndices);
        }
        public void UnloadSceneAsync(int buildIndex, [NotNull] Action<AsyncOperation> onCompleteFunc)
        {
            if (IsSceneLoadedAndNotUnloading(buildIndex))
            {
                var unload_scene_async = CreateAsyncSceneUnload(buildIndex);
                unload_scene_async.allowSceneActivation = true;
                unload_scene_async.completed += onCompleteFunc;
            }
        }
        public void QuitApplication(int exitCode = 0)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(exitCode);
#endif
        }

        private AsyncOperation CreateAsyncSceneLoad(int buildIndex, LoadSceneMode loadSceneMode)
        {
            var async_scene_load = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(buildIndex, loadSceneMode);
            _asyncSceneLoads.Add(buildIndex, async_scene_load);
            async_scene_load.completed += (_) => { RemoveSceneLoad(buildIndex); };
            return async_scene_load;
        }
        private AsyncOperation CreateAsyncSceneUnload(int buildIndex)
        {
            var async_scene_unload = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(buildIndex);
            _asyncSceneUnloads.Add(buildIndex, async_scene_unload);
            async_scene_unload.completed += (_) => { RemoveSceneUnload(buildIndex); };
            return async_scene_unload;
        }

        private void RemoveSceneUnload(int buildIndex)
        {
            _asyncSceneUnloads.Remove(buildIndex);
        }

        private void RemoveSceneLoad(int buildIndex)
        {
            _asyncSceneLoads.Remove(buildIndex);
        }
        private float CalculateCurrentAsyncLoadProgress()
        {
            float progress_sum = 0;
            foreach (var scene_load in _asyncSceneLoads.Values)
            {
                progress_sum += scene_load.progress;
            }
            return progress_sum / _asyncSceneLoads.Count;
        }
        private float CalculateCurrentAsyncLoadProgress(int[] sceneIndices)
        {
            float progress_sum = 0;
            foreach (var scene_load in _asyncSceneLoads)
            {
                if (sceneIndices.Contains(scene_load.Key)) progress_sum += scene_load.Value.progress;
            }
            return progress_sum / _asyncSceneLoads.Count;
        }
    }
}
