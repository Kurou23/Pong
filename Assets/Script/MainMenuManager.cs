using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [Header("Main Menu Panel List")]
    public GameObject StartPanel;
    public GameObject HTPPanel;
    public GameObject TimerPanel;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartPanel.SetActive(true);
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
    }

    public void SinglePlayerButton() {
        GameData.instance.isSinglePlayer = true;
        TimerPanel.gameObject.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }
    public void MultiPlayerButton()
    {
        GameData.instance.isSinglePlayer = false;
        TimerPanel.gameObject.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void ClosePanelHTP() {
        HTPPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void CloseTimerPanel() {
        TimerPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void StartBtn() {
        SceneManager.LoadScene("2. Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void SetTimerButton(float Timer) {
        GameData.instance.gameTimer = Timer;
        HTPPanel.SetActive(true);
        TimerPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

}
