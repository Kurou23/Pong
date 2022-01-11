using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float delayStart;
    public int player1Score;
    public int player2Score;

    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI player1ScoreTxt;
    public TextMeshProUGUI player2ScoreTxt;

    public GameObject Ball;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }


    private void Start()
    {
        // Manggil Function yg Pake IEnumerator harus pake Start Coroutine kalo gk kga bakal jalan
        StartCoroutine("DelayStart", delayStart);
    }

    public void StartGame()
    {
        Instantiate(Ball, Vector3.zero, Quaternion.identity);
    }

    // Buat bikin Delay Start
    private IEnumerator DelayStart(float timer)
    {
        // bakal otomatis ngitung mundur dari timer yang di set ke 0 
        yield return new WaitForSeconds(timer);
        StartGame();
    }
}
