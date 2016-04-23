using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
