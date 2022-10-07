using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Developers.Vitor.Scripts.Utilities;
using Bagunca.Organizar;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace _Developers.Vitor.Scripts.Managers
{
    public class MySceneLoader : PersistentSingleton<MySceneLoader>
    {
        
        public UnityAction sceneLoaded;
        public UnityAction sceneUnloaded;
        public float totalSceneProgress { get; private set; }
        private List<AsyncOperation> scenesLoading = new();
        private List<AsyncOperation> scenesUnloading = new();
        [SerializeField] private GameObject uiLoadPrefab;
        private UiLoadPanel _uiLoadPanel;
        public void LoadScenes(int[] scenes)
        {
            OpenLoadPanel();
            foreach (var scene in scenes)
            {
                if (!SceneManager.GetSceneByBuildIndex(scene).isLoaded)
                {
                    scenesLoading.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single));
                }
            }
            StartCoroutine(GetSceneLoadProgress(scenesLoading));
        }
        
        private IEnumerator GetSceneLoadProgress(List<AsyncOperation> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                while (!scenesLoading[i].isDone)
                {
                    totalSceneProgress = 0;
                    foreach (AsyncOperation operation in list)
                    {
                        totalSceneProgress += operation.progress;
                    }
                    totalSceneProgress = (totalSceneProgress / list.Count) * 100f;
                    _uiLoadPanel.loadSlider.value = totalSceneProgress;
                    yield return null;
                }
            }   
            // Debug.Log("batata");
            _uiLoadPanel.loadSlider.value = 100;
            yield return new WaitForSeconds(1.5f);
            _uiLoadPanel.gameObject.SetActive(false);
            sceneLoaded?.Invoke();
        }
        
        private IEnumerator GetSceneUnloadProgress(List<AsyncOperation> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                while (!scenesLoading[i].isDone)
                {
                    totalSceneProgress = 0;
                    foreach (AsyncOperation operation in list)
                    {
                        totalSceneProgress += operation.progress;
                    }
                    totalSceneProgress = (totalSceneProgress / list.Count) * 100f;
                    yield return null;
                }
            }
            yield return new WaitForSeconds(0.5f);
            sceneUnloaded?.Invoke();
        }
        
        public void UnloadAnotherScenes(int[] scenesToIgnore)
        {
            int temp = SceneManager.sceneCount;
            for (int i = 0; i < temp; i++)
            {
                if (!scenesToIgnore.Contains(SceneManager.GetSceneAt(i).buildIndex))
                {
                    scenesUnloading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i)));
                }
            }
            StartCoroutine(GetSceneUnloadProgress(scenesUnloading));
        }

        public void HideLoadPanel()
        {
            _uiLoadPanel.gameObject.SetActive(false);
        }

        public void OpenLoadPanel()
        {
            if (_uiLoadPanel == null)
            {
                _uiLoadPanel = Instantiate(uiLoadPrefab,transform).GetComponent<UiLoadPanel>();
            }
            _uiLoadPanel.loadSlider.value = 0;
            _uiLoadPanel.SetRandomImage();
            _uiLoadPanel.gameObject.SetActive(true);
        }
    }
}