using UnityEngine;
using TMPro;

public class PlayerPrefText : MonoBehaviour
{
    
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score") + "";

    }
}
