using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class MySceneManager : MonoSingleton<MySceneManager>
{
    //Will change later
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private ProgressBar progressBar;
    private static int currentLevelIndex = (int)SceneIndexes.LEVELS;
    private List<AsyncOperation> scenesUnLoading = new List<AsyncOperation>();
    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    void Start()
    {
        //if (SceneManager.sceneCount != SceneManager.sceneCountInBuildSettings) FirstLoad();
    }
    [Button]
    public void RestartActiveScene()
    {
        UnloadCurrentLevel();
        var sceneToLoad = LoadLevel(currentLevelIndex);
        StartCoroutine(WaitForAllScenes(sceneToLoad));
    }

    [Button]
    public void LoadNextLevel()
    {
        UnloadCurrentLevel();
        currentLevelIndex++;
        var sceneToLoad = LoadLevel(currentLevelIndex);
        StartCoroutine(WaitForAllScenes(sceneToLoad));
    }

    private void UnloadCurrentLevel(bool restartEnv = false)
    {
        //RESET UI, GAMEMANAGER ETC
        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentLevelIndex));
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.ENVIRONMENT));
        
    }
    private AsyncOperation LoadLevel(int index)
    {
        //SETUP UI, GAMEMANAGER ETC
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        scenesLoading.Add(asyncLoadScene);
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.ENVIRONMENT, LoadSceneMode.Additive));

        return asyncLoadScene;
        //StartCoroutine(WaitForSceneLoad(asyncLoadScene));
    }
    private void FirstLoad()
    {
        var gameScene = SceneManager.LoadSceneAsync(currentLevelIndex, LoadSceneMode.Additive);
        var uiScene = SceneManager.LoadSceneAsync((int)SceneIndexes.UI, LoadSceneMode.Additive);

        scenesLoading.Add(gameScene);
        scenesLoading.Add(uiScene);
        //var mainScene = SceneManager.LoadSceneAsync((int)SceneIndexes.MAINSCENE, LoadSceneMode.Additive);

        StartCoroutine(WaitForAllScenes(gameScene));
    }

    private IEnumerator WaitForAllScenes(AsyncOperation sceneToSetActive)
    {
        float totalSceneProgress = 0;

        loadingScreen.SetActive(true);
        for(int i = 0;i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }
                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                progressBar.SetCurrentFill(Mathf.RoundToInt(totalSceneProgress));
                yield return null;
            }
        }
        loadingScreen.SetActive(false);
        Debug.Log("Setting active scene..");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevelIndex));
    }
}


