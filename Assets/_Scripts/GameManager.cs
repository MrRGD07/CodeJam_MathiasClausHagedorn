using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool initiateGame;
    [SerializeField] private bool gameStarted;
    private int[] playerScores = new int[3];

    [SerializeField] private TextMeshProUGUI gameoverPanelScoreText;
    [SerializeField] private TextMeshProUGUI gameoverPanelScoreText1st;
    [SerializeField] private TextMeshProUGUI gameoverPanelScoreText2nd;
    [SerializeField] private TextMeshProUGUI gameoverPanelScoreText3rd;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gamePanel;

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 startPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        LoadScore();
    }

    public void Update()
    {
        Game();
    }

    public bool GetGameStatus()
    {
        return gameStarted;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        GameEnded();
        DestroyAllObject();
        mainMenuPanel.SetActive(true);
        gameoverPanel.SetActive(false);
        CloseSettings();
        player.transform.position = new Vector3(0, -5, 0) + startPos;
    }

    public void StartGame()
    {
        //Reset score
        score = 0;

        //Start Game
        initiateGame = true;
        gameoverPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    public void GameEnded()
    {
        gameStarted = false;
        initiateGame = false;

        //Enable gameover panel
        gameoverPanel.SetActive(true);
        gamePanel.SetActive(false);

        UpdateScore(score);
        gameoverPanelScoreText.text = $"Your score\n{score.ToString("0")}m";
        gameoverPanelScoreText1st.text = $"1st:\n{playerScores[0].ToString("0")}m";
        gameoverPanelScoreText2nd.text = $"2nd:\n{playerScores[1].ToString("0")}m";
        gameoverPanelScoreText3rd.text = $"3rd:\n{playerScores[2].ToString("0")}m";
        SaveScore();

        //Stop object spawning
        SpawnManager.instance.StopSpawning();

        Time.timeScale = 0;
    }
    public void UpdateScore(float value)
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
            if (value > playerScores[i])
            {
                playerScores[i] = Mathf.RoundToInt(value);
                break;
            }
        }
    }
    public void SaveScore()
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), playerScores[i]);
        }
    }
    public void LoadScore()
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = PlayerPrefs.GetInt(i.ToString());
        }
    }
    public void Game()
    {
        if (!initiateGame) return;

        if (player.transform.position != startPos && !gameStarted)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, startPos, 0.02f);

            if (player.transform.position.y > startPos.y - 0.01f)
            {
                gameStarted = true;
            }
        }

        if (!gameStarted) return;
        score += Time.deltaTime;
        scoreText.text = $"{score.ToString("0")}m";

        if (!SpawnManager.instance.GetSpawningStatus())
        {
            gamePanel.SetActive(true);
            SpawnManager.instance.StartSpawning();
        }
    }

    public void DestroyAllObject()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}