using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Stack<int> sceneHistory = new Stack<int>();
    private int currSceneIndex;

    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        if (Instance != null) SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currSceneIndex = scene.buildIndex;
        sceneHistory.Push(currSceneIndex);
    }

    public void LoadScene(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadPreviousScene()
    {
        if (sceneHistory.Count > 1)
        {
            sceneHistory.Pop();
            currSceneIndex = sceneHistory.Pop();
            LoadScene(currSceneIndex);
        }
    }

    public void ReloadCurrentScene()
    {
        LoadScene(sceneHistory.Pop());
    }

    public int GetCurrentSceneIndex()
    {
        return currSceneIndex;
    }
}
