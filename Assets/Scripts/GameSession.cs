using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    

    void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = "Lives: " + playerLives;
        scoreText.text = "Score: " + score;
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    private void TakeLife()
    {
        playerLives -= 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = "Lives: " + playerLives;
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScreenPersist>().Reset();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
