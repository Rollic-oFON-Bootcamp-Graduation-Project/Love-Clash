using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoSingleton<MySceneManager>
{
    private static int currentLevelIndex = (int)SceneIndexes.LEVELS;
    private List<AsyncOperation> scenesUnLoading = new List<AsyncOperation>();
  
    void Start()
    {
        //if (SceneManager.sceneCount != SceneManager.sceneCountInBuildSettings) LoadScenes((int)SceneIndexes.Game);
    }
    public void RestartActiveScene()
    {
        UnloadActiveScene();
        LoadScenes(currentLevelIndex);
    }

    private void UnloadActiveScene(bool isRestartScene = false)
    {
        //RESET UI, GAMEMANAGER ETC
        scenesUnLoading.Add(SceneManager.UnloadSceneAsync(currentLevelIndex));
        StartCoroutine(WaitForSceneUnload());
    }
    private void LoadScenes(int index)
    {
        //SETUP UI, GAMEMANAGER ETC
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        StartCoroutine(WaitForSceneLoad(asyncLoadScene));
    }
    private void FirstLoad()
    {
        SceneManager.LoadSceneAsync(currentLevelIndex, LoadSceneMode.Additive);
        AsyncOperation asyncLoadScene =  SceneManager.LoadSceneAsync((int)SceneIndexes.UI, LoadSceneMode.Additive);
        StartCoroutine(WaitForSceneLoad(asyncLoadScene));
    }
    public IEnumerator WaitForSceneLoad(AsyncOperation scene)
    {
        while (!scene.isDone)
        {
            yield return null;
        }
        Debug.Log("Setting active scene..");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentLevelIndex));
    }
    public IEnumerator WaitForSceneUnload()
    {
        for (int i = 0; i < scenesUnLoading.Count; i++)
        {
            while (!scenesUnLoading[i].isDone)
            {
                yield return null;
            }
        }
    }
}


