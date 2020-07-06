using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gameOverController : MonoBehaviour
{
    public GameObject gamemanager;

    public Text highScore;
    public Text currentScore;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.GetComponent<CanvasGroup>().alpha);
    }

    public void hideui()
    {
        
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }
    public void ShowUi()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void SetUi(int score)
    {
        currentScore.text = score.ToString();
        highScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    public void ReloadScene()
    {
        gamemanager.GetComponent<GameManager>().ReloadGame();
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
