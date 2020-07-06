using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    CanvasGroup[] allChildren;

    public GameObject mainCamera;
    public GameObject gameManager;
    public GameObject player;

    public Slider slider;

    public Text invertText;
    public Text audioLevel;

    public float rotateSpeed = 12f;


    public int test;

    private bool invert = false;
    private bool rotateDown = false;
    // Start is called before the first frame update
    void Start()
    {
        allChildren = gameObject.GetComponentsInChildren<CanvasGroup>();
        audioLevel.text = Mathf.RoundToInt(PlayerPrefs.GetFloat("VolumeLevel")).ToString();
        slider.value = PlayerPrefs.GetFloat("VolumeLevel");
        if (PlayerPrefs.GetInt("InvertControls") != 1)
        {
            invertText.text = "Normal";
        }
        else
        {
            invertText.text = "Inverted";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mainCamera.GetComponent<Transform>().localRotation.x < 0.034f && rotateDown)
        {
            mainCamera.GetComponent<Transform>().localRotation *= Quaternion.Euler(rotateSpeed * Time.deltaTime, 0f, 0f);
        }
       
    }


    public void PlayGame()
    {
        rotateDown = true;
        player.GetComponent<PlayerController>().gameStarted = true;
        allChildren[1].alpha = 0f;
        allChildren[1].blocksRaycasts = false;
        allChildren[2].alpha = 0f;
        allChildren[1].blocksRaycasts = false;
        gameManager.GetComponent<GameManager>().StartGame();
    }

    public void OpenOptions()
    {
        allChildren[1].alpha = 0f;
        allChildren[1].blocksRaycasts = false;
        allChildren[2].alpha = 1f;
        allChildren[2].blocksRaycasts = true;
    }
    public void CloseOptions()
    {
        allChildren[2].alpha = 0f;
        allChildren[2].blocksRaycasts = false;
        allChildren[1].alpha = 1f;
        allChildren[1].blocksRaycasts = true;
    }
    
    public void OpenInstructions()
    {
        allChildren[1].alpha = 0f;
        allChildren[1].blocksRaycasts = false;
        allChildren[3].alpha = 1f;
        allChildren[3].blocksRaycasts = true;
    }

    public void CloseInstructions()
    {
        allChildren[1].alpha = 1f;
        allChildren[1].blocksRaycasts = true;
        allChildren[3].alpha = 0f;
        allChildren[3].blocksRaycasts = false;
    }
    public void InvertControls()
    {
        if (PlayerPrefs.GetInt("InvertControls") != 1)
        {
            PlayerPrefs.SetInt("InvertControls", 1);
            player.GetComponent<PlayerController>().invertedVertical = true;
            invertText.text = "Inverted";
        }
        else
        {
            PlayerPrefs.SetInt("InvertControls", 0);
            player.GetComponent<PlayerController>().invertedVertical = false;
            invertText.text = "Normal";
        }
        
    }

        

    public void OnValueChanged(float newValue)
    {
        PlayerPrefs.SetFloat("VolumeLevel", newValue);
        float tempValue = Mathf.RoundToInt((newValue * 100));
        mainCamera.GetComponent<AudioSource>().volume = newValue;
        audioLevel.text = tempValue.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
