using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int lives = 3;
    private int fliesCollected = 0;
    [SerializeField] private Text livesDisplay = default;
    [SerializeField] private Text fliesDisplay = default;

    [SerializeField] private GameObject scorePanel = default;

    [SerializeField] private GameObject gameOverMenu = default;

    [SerializeField] private Button playAgainButton = default;
    [SerializeField] private Text gameOverScore = default;
    [SerializeField] private Camera introCamera = default;
    [SerializeField] private Camera mainCamera = default;
    [SerializeField] private Image introImage = default;
    [SerializeField] private GameObject buttons = default;
    [SerializeField] private Button playButton = default;
    [SerializeField] private Button optionsButton = default;
    [SerializeField] private Button quitButton = default;

    void Start()
    {
        introCamera.enabled = true;
        mainCamera.enabled = false;
        playAgainButton.onClick.AddListener(PlayAgain);
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
        scorePanel.SetActive(false);
        gameOverMenu.SetActive(false);
        buttons.SetActive(false);
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        livesDisplay.text = lives.ToString();
        gameOverScore.text = fliesCollected.ToString();
        fliesDisplay.text = fliesCollected.ToString();
    }

    //prebacit u player cont
    public void LooseLife()
    {
        lives--;
        if (lives == 0)
        {
            GameOver();
        }
    }

    //prebacit u player cont
    public void CollectFly()
    {
        fliesCollected++;
    }

    public void GameStart()
    {
        StartCoroutine(Started());
    }

    IEnumerator Started()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeImage(introImage));
        yield return new WaitForSeconds(1f);
        introImage.gameObject.SetActive(false);
        buttons.SetActive(true);
        StartCoroutine(FadeImageIn(playButton.image));
        StartCoroutine(FadeImageIn(optionsButton.image));
        StartCoroutine(FadeImageIn(quitButton.image));
    }

    private void Play()
    {
        buttons.SetActive(false);
        introCamera.enabled = false;
        mainCamera.enabled = true;
        scorePanel.SetActive(true);
    }

    IEnumerator FadeImage(Image img)
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator FadeImageIn(Image img)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        AudioManager.I.Play("Sparkle");
        gameOverMenu.SetActive(false);
        scorePanel.SetActive(true);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void GameOver()
    {
        scorePanel.SetActive(false);
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Quit()
    {

    }
}
