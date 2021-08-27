using System.Collections.Generic;
using ViewpointSwitcher.Editor.DataClasses;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ViewpointSwitcher.Editor
{
    [InitializeOnLoad]
    public class ViewpointSwitcherToolbar
    {
        private const string STORAGE_ASSET_PATH = "Assets/Plugins/ViewpointSwitcher/Editor/ViewpointSwitcherStorage.asset";
    
        [SerializeField] private static ViewpointSwitcherStorage _storage;

        private static string[] _availablePointOptionNames;
        private static List<CameraPosition> _availablePointOptions;
        private static int _currentPositionIndex = 0;
        private static bool HasPositionSelected => _availablePointOptions != null && _currentPositionIndex < _availablePointOptions.Count;

        private static string _currentlyEnteredName;

        private static string CurrentlyActiveScenePath => CurrentlyActiveScene.path;
        private static Scene CurrentlyActiveScene => EditorSceneManager.GetActiveScene();


        static ViewpointSwitcherToolbar()
        {
            ToolbarExtender.Editor.ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
            EditorSceneManager.activeSceneChangedInEditMode += HandleSceneOpened;

            LoadStorageFromAssets();
            
            var scene = CurrentlyActiveScene;
            HandleSceneOpened(scene, scene);
        }
        private static void LoadStorageFromAssets()
        {
            _storage = AssetDatabase.LoadAssetAtPath<ViewpointSwitcherStorage>(STORAGE_ASSET_PATH);
            if (_storage == null)
            {
                //Debug.Log("no storage found, creating . . .");
                _storage = ScriptableObject.CreateInstance<ViewpointSwitcherStorage>();
                AssetDatabase.CreateAsset(_storage, STORAGE_ASSET_PATH);
                EditorUtility.SetDirty(_storage);
                AssetDatabase.SaveAssets();
            }
            else
            {
                //Debug.Log($"storage found, has points for {_storage.ViewpointDict.Count} scenes");
            }
        }
        
        private static void OnToolbarGUI()
        {
            if (_storage == null) LoadStorageFromAssets();
            
            GUILayout.FlexibleSpace();
            
            EditorGUI.BeginChangeCheck();
            _currentPositionIndex = EditorGUILayout.Popup(_currentPositionIndex, _availablePointOptionNames);
            
            if(EditorGUI.EndChangeCheck() && GetCurrentlySelectedCameraPosition(out CameraPosition newly_selected_camera_position))
                SetSceneViewCameraPosition(newly_selected_camera_position);

            bool was_asset_changed = false;
            
            _currentlyEnteredName = GUILayout.TextField(_currentlyEnteredName, GUILayout.Width(100));
            using (new EditorGUI.DisabledScope(!CanAddCurrentCameraPosition(out CameraPosition camera_position)))
                if (GUILayout.Button("+"))
                {
                    AddCameraPosition(camera_position);
                    was_asset_changed = true;
                }

            using (new EditorGUI.DisabledScope(!HasPositionSelected))
                if (GUILayout.Button("-"))
                {
                    RemoveCameraPosition();
                    was_asset_changed = true;
                }
            
            GUILayout.FlexibleSpace();
            
            if(was_asset_changed)
            {
                EditorUtility.SetDirty(_storage);
                AssetDatabase.SaveAssets();
            }
        }

        private static void HandleSceneOpened(Scene scene, Scene newlyOpenedScene)
        {
            if (!_storage.ViewpointDict.TryGetValue(newlyOpenedScene.path, out _availablePointOptions))
            {
                _availablePointOptions = null;
            }
            RegenerateOptionNames();
        }
        private static void RegenerateOptionNames()
        {
            if (_availablePointOptions != null && _availablePointOptions.Count > 0)
            {
                _availablePointOptionNames = new string[_availablePointOptions.Count];
                for (int i = 0; i < _availablePointOptions.Count; i++)
                {
                    _availablePointOptionNames[i] = _availablePointOptions[i].name;
                }
            }
            else
            {
                _availablePointOptionNames = new[]{"No Viewpoints Stored"};
            }

            if (!HasPositionSelected)
            {
                _currentPositionIndex = 0;
            }
        }

        private static bool CanAddCurrentCameraPosition(out CameraPosition cameraPosition)
        {
            return !(!GetSceneViewCameraPosition(out cameraPosition) || string.IsNullOrWhiteSpace(_currentlyEnteredName) || (_availablePointOptions != null && _availablePointOptions.Contains(cameraPosition)));
        }
        private static void AddCameraPosition(CameraPosition position)
        {
            //Debug.Log("adding camera position");
            if (_availablePointOptions != null)
            {
                _availablePointOptions.Add(position);
                //Debug.Log("list exists, adding position");
            }
            else
            {
                _availablePointOptions = new List<CameraPosition> {position};
                _storage.ViewpointDict.Add(CurrentlyActiveScenePath, _availablePointOptions);
                //Debug.Log("list was empty, adding new to dict");
            }

            _currentPositionIndex = _availablePointOptions.Count - 1;
            RegenerateOptionNames();
        }
        private static void RemoveCameraPosition()
        {
            //Debug.Log("removing camera position");
            _availablePointOptions.RemoveAt(_currentPositionIndex);
            RegenerateOptionNames();
        }
        
        private static void SetSceneViewCameraPosition(CameraPosition cameraPosition, bool direct = false)
        {
            var scene_view = SceneView.lastActiveSceneView;
            if (scene_view == null) return;
            
            if(direct) scene_view.LookAtDirect(cameraPosition.pivot, cameraPosition.rotation, cameraPosition.size);
            else scene_view.LookAt(cameraPosition.pivot, cameraPosition.rotation, cameraPosition.size);
        }
        private static bool GetSceneViewCameraPosition(out CameraPosition cameraPosition)
        {
            var scene_view = SceneView.lastActiveSceneView;
            if (scene_view == null)
            {
                cameraPosition = default;
                return false;
            }

            cameraPosition = new CameraPosition(_currentlyEnteredName, scene_view.pivot, scene_view.rotation, scene_view.size);
            return scene_view != null;
        }
        private static bool GetCurrentlySelectedCameraPosition(out CameraPosition selectedCameraPosition)
        {
            if(HasPositionSelected)
            {
                selectedCameraPosition = _availablePointOptions[_currentPositionIndex];
                return true;
            }

            selectedCameraPosition = default;
            return false;
        }

    }
}
