using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int score=0;
    public Text scoreText;
    public GameObject playButton;
    public GameObject GameOver;
    public Player Player;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        GameOver.SetActive(false);

        Time.timeScale = 1f;
        Player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for(int i=0;i<pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        Player.enabled = false;
    }

    public void increateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void gameOver()
    {
        GameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }
}
