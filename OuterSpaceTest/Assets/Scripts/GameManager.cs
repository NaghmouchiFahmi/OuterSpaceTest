using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI ScoreUi;
    public TextMeshProUGUI JumpsUi;
    public TextMeshProUGUI GameOverUi;
    public TextMeshProUGUI GameWonUi;

    public GameObject GameOverUIObject;
    public GameObject GameWonUIObject;

    public int JumpCount = 3;
    public int score = 0;

    public GameObject[] Asteroids;


   
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
        if (JumpsUi != null)
        {
            JumpsUi.text = JumpCount.ToString();
        }
        if (ScoreUi != null)
        {
            ScoreUi.text = "Score : " + score.ToString();
        }


        if (JumpCount <= 0)
        {
            Time.timeScale = 0f;
            GameOverUIObject.SetActive(true);
            GameManager.Instance.GameOverUi.text = "Score: " + score.ToString();
        }
      

        Asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        StartCoroutine(SpawningStopCoroutine());



    }

    private IEnumerator SpawningStopCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (Asteroids.Length <= 0)
        {
            Time.timeScale = 0f;
            ScoreUi.gameObject.SetActive(false);
            JumpsUi.gameObject.SetActive(false);
            GameWonUi.text = "Score : " + score.ToString();
            GameWonUIObject.SetActive(true);

        }


    }

   
public void AddScore(int points)
    {
        score += points;
        
    }


    public void RestartScene()
    {
        Time.timeScale = 1f;
        ScoreUi.gameObject.SetActive(false);
        JumpsUi.gameObject.SetActive(false);
        SceneManager.LoadScene("Level1");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("Level2");
    }
}
