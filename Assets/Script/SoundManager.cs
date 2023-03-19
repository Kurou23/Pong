using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip uiButton;
    public AudioClip ballBounce;
    public AudioClip goal;
    public AudioClip gameOver;
    public AudioSource BGM;
    public Image ImgMuteButton;
    public Sprite[] MuteSprite;

    private AudioSource audio;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameOver);
        else
            instance = this;

        audio = GetComponent<AudioSource>();

        audio.mute = GameData.instance.isMute;
        BGM.mute = GameData.instance.isMute;

    }

    public void UIClickSfx()
    {
        audio.PlayOneShot(uiButton);
    }

    public void BallBounceSfx()
    {
        audio.PlayOneShot(ballBounce);
    }

    public void GoalSfx()
    {
        audio.PlayOneShot(goal);
    }

    public void GameOverSfx()
    {
        audio.PlayOneShot(gameOver);
    }

    public void MuteButton()
    {
        audio.mute = !audio.mute;
        BGM.mute = !BGM.mute;
        if (audio.mute)
        {
            ImgMuteButton.sprite = MuteSprite[1];
        }
        else
        {
            ImgMuteButton.sprite = MuteSprite[0];
        }
        GameData.instance.isMute = audio.mute;
    }


}
