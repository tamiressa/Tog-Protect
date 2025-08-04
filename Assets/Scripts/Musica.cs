using UnityEngine;
using UnityEngine.SceneManagement;

public class Musica : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        
    }
}
