using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        LevelSaver.Init();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void ContinueGame()
    {
        int activeScene = LevelSaver.LoadData().SelectedLevel;
        SceneManager.LoadScene(activeScene);
    }

    public void ExitGame()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
}
