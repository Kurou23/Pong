using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Deklarasi tipe data dan variabel
    [Header("Game Settings")]
    public float delayStart;
    public int player1Score;
    public int player2Score;
    public float timer;
    public bool isOver;
    public bool goldenGoal;
    public GameObject Ball;
    public GameObject[] PowerUp;

    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("InGame UI")]
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI player1ScoreTxt;
    public TextMeshProUGUI player2ScoreTxt;
    public GameObject goldenGoalUI;

    [Header("Game Over UI")]
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    public GameObject youWin;
    public GameObject youLose;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);

        player2WinUI.SetActive(false);
        player1WinUI.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        goldenGoalUI.SetActive(false);

        // Manggil Function yg Pake IEnumerator harus pake Start Coroutine kalo gk kga bakal jalan
        StartCoroutine("DelayStart", delayStart);
        timer = GameData.instance.gameTimer;
        isOver = false;
        goldenGoal = false;

        StartCoroutine("SpawnPowerUp");
    }

    private void Update()
    {
        player1ScoreTxt.text = player1Score.ToString();
        player2ScoreTxt.text = player2Score.ToString();
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if (timer <= 0f && !isOver)
        {
            timerTxt.text = "00:00";
            if (player1Score == player2Score)
            {
                if (!goldenGoal)
                {
                    goldenGoal = true;

                    Ball[] ball = FindObjectsOfType<Ball>();
                    for (int i = 0; i < ball.Length; i++)
                    {
                        Destroy(ball[i].gameObject);
                    }

                    goldenGoalUI.SetActive(true);

                    SpwanBall();
                }
            }
            else
            {
                GameOver();
            }

        }
    }

    public void StartGame()
    {
        Instantiate(Ball, Vector3.zero, Quaternion.identity);
    }

    public void SpwanBall()
    {
        StartCoroutine("DelaySpawn");
    }

    public IEnumerator SpawnPowerUp() {
        yield return new WaitForSeconds(10f);
        Debug.Log("Power Up");
        int rand = Random.Range(0,PowerUp.Length-1);
        Instantiate(PowerUp[rand], new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-2.35f, 2.25f),0), Quaternion.identity);
        StartCoroutine("SpawnPowerUp");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.UIClickSfx();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("1. Main Menu");
        SoundManager.instance.UIClickSfx();
    }

    public void GameOver()
    {
        SoundManager.instance.GameOverSfx();
        isOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0;

        GameOverPanel.SetActive(true);

        if (!GameData.instance.isSinglePlayer)
        {
            if (player1Score > player2Score)
            {
                player1WinUI.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                player2WinUI.SetActive(true);
            }
        }
        else
        {
            if (player1Score > player2Score)
            {
                youWin.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                youLose.SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
        SoundManager.instance.UIClickSfx();
        Time.timeScale = 1f;

        // Destroy Semua bola di lapangan
        Ball[] ball = FindObjectsOfType<Ball>();
        for (int i = 0; i < ball.Length; i++)
        {
            Destroy(ball[i].gameObject);
        }

        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);

        player2WinUI.SetActive(false);
        player1WinUI.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        goldenGoalUI.SetActive(false);

        // Manggil Function yg Pake IEnumerator harus pake Start Coroutine kalo gk kga bakal jalan
        StartCoroutine("DelayStart", delayStart);
        timer = GameData.instance.gameTimer;
        isOver = false;
        goldenGoal = false;
        player1Score = 0;
        player2Score = 0;
    }

    // Buat bikin Delay Start
    private IEnumerator DelayStart(float timer)
    {
        // bakal otomatis ngitung mundur dari timer yang di set ke 0 
        yield return new WaitForSeconds(timer);
        StartGame();
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        Instantiate(Ball, Vector3.zero, Quaternion.identity);
    }
}
