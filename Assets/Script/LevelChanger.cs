using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private int _activeSceneIndex;
    private int _sceneCount;
    private bool? _levelStatus;
    private void Awake()
    {
        _activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _sceneCount = SceneManager.sceneCountInBuildSettings;
        LevelStatus.SetSaveData();
        LevelStatus.SetActiveSceneIndex(_activeSceneIndex);
        LevelStatus.SetSceneCount(_sceneCount);
        LevelStatus.ChangeLevelResult(null);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_activeSceneIndex);
    }
    
    public void NextLevel()
    {
        Time.timeScale = 1f;
        if (_sceneCount == _activeSceneIndex + 1)
        {
            Home();
        }
        else
        {
            SceneManager.LoadScene(_activeSceneIndex + 1);
        }
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    
}
