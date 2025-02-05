using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text gameOverText;
    public Text continueText;
    public Text PauseText;
    public GameObject musicSlider;
    public GameObject soundSlider;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup soundMixer;
    public AudioSource testAudio;
    public MeteorMover meteorMover;
    public MeteorMover metalMeteorMover;
    [SerializeField] Sprite bulletStart;
    [SerializeField] SpriteRenderer bulletSprite;

    public int playerScore = 0;

    AsyncOperation asyncOp;
    float progress;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        meteorMover.speed = -2f;
        metalMeteorMover.speed = -2f;
    }

    private void Start()
    {
        StartCoroutine(LoadNewSceneAsync());
        bulletSprite.sprite = bulletStart;
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume", 1);
        soundSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundsVolume", 1);
    }

    public void AddScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
    }

    public void PlayerDied()
    {
        gameOverText.enabled = true;
        Invoke("GameOver", 2);
    }

    public void GameOver()
    {
        continueText.enabled = true;
        Time.timeScale = 0;
    }


    public void Reset()
    {
        ActivateScene();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (continueText.enabled)
        {
            foreach (Touch touch in Input.touches)
            {
                Reset();
            }
        }
    }

    public void Pause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            PauseText.enabled = true;
            musicSlider.SetActive(true);
            soundSlider.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseText.enabled = false;
            musicSlider.SetActive(false);
            soundSlider.SetActive(false);
        }
    }

    void ActivateScene()
    {
        asyncOp.allowSceneActivation = true;
    }

    IEnumerator LoadNewSceneAsync()
    {
        asyncOp = SceneManager.LoadSceneAsync(1);
        asyncOp.allowSceneActivation = false;
        while (!asyncOp.isDone)
        {
            // При allowSceneActiovation = false; 
            // максимальное значение async.progress - 0.9f
            progress = asyncOp.progress / 0.9f;
            yield return null;
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void ChangeSoundsVolume(float volume)
    {
        while (Input.touchCount > 0 && !testAudio.isPlaying)
        {
            testAudio.Play();
            break;
        }
        musicMixer.audioMixer.SetFloat("SoundsVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundsVolume", volume);
    }
}
