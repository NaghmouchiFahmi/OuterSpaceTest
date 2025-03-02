using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI ScoreUi;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(ScoreUi != null)
        {
            ScoreUi.text = "Score : " + score.ToString();
        }
        
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }


    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
