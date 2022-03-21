using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject enemyPrefab;
    public GameObject bestTime;
    public GameObject mainMenuButton;
    public GameObject startButton;
    public GameObject resumeButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;

    public bool isGameover;
    public bool isLeft;
    public int enemylife;
    public int playerLife;
    public int enemyCount;
    public int score;
    public int waveDifficulty;

    private bool isText;
    private int columns = 8;
    private int rows = 5;
    private int minutes;
    private int seconds;
    private int bestTimeMinutes;
    private int bestTimeSeconds;
    private float xLimit = 15;
    private float zPos = 10;
    private float startDelay = 2;
    private float repeatRate = 1.5f;
    private float gapOffset = 2;
    private float timeCount;
    private float bestTimeCount;

    void Start()
    {
        isGameover = true;
        playerLife = 3;
        bestTimeCount = 100;
        timeCount = 0;
        waveDifficulty = 0;
        score = 0;
        enemylife = 1;
        isLeft = true;

        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        spawnEnemy();
    }

    void Update()
    {
        if (!isGameover)
        {
            Timer();
            CreateEnemy();
            DisplayUI();
        }
    }

    public void StartGame()
    {
        isGameover = false;
        startButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        resumeButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        resumeButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    void SpawnObstacle()
    {
        float randomXPos = Random.Range(-xLimit, xLimit);
        Vector3 spawnObstacle = new Vector3(randomXPos, 0.5f, zPos);
        Instantiate(obstaclePrefab, spawnObstacle, obstaclePrefab.transform.rotation);
    }

    void Timer()
    {
        timeCount += Time.deltaTime;
        minutes = Mathf.FloorToInt(timeCount / 60);
        seconds = Mathf.FloorToInt(timeCount % 60);

        bestTimeMinutes = Mathf.FloorToInt(bestTimeCount / 60);
        bestTimeSeconds = Mathf.FloorToInt(bestTimeCount % 60);
    }

    void CreateEnemy()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            if (timeCount < bestTimeCount)
            {
                bestTimeCount = timeCount;
                isText = true;
            }
            timeCount = 0;
            waveDifficulty++;
            if (waveDifficulty % 4 == 0)
            {
                enemylife++;
            }
            columns = Random.Range(5, 10);
            rows = Random.Range(3, 6);
            gapOffset = Random.Range(1.5f, 2.5f);
            spawnEnemy();
        }
    }

    void DisplayUI()
    {
        if (isText == true)
        {
            bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}", bestTimeMinutes, bestTimeSeconds);
            bestTime.SetActive(true);
            StartCoroutine(BestTime());
        }
        lifeText.text = "Life: " + playerLife;
        scoreText.text = "Score: " + score;
        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void spawnEnemy()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Instantiate(enemyPrefab, new Vector3((i * gapOffset), 0.5f, (j * 2) - 1), enemyPrefab.transform.rotation);
            }
        }
    }

    IEnumerator BestTime()
    {
        yield return new WaitForSeconds(4);
        isText = false;
        bestTime.SetActive(false);
    }
}
