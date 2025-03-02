using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public TextMeshProUGUI loadingText; 

    void Start()
    {
        StartCoroutine(AnimateLoadingText());
        StartCoroutine(LoadNextScene()); 
    }

    IEnumerator AnimateLoadingText()
    {
        string baseText = "Loading";
        while (true)
        {
            for (int i = 0; i <= 3; i++)
            {
                loadingText.text = baseText + new string('.', i);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Game"); 
    }
}
