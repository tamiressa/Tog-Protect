using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
    }

    public void Play(int index)
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(index);
    }
}
