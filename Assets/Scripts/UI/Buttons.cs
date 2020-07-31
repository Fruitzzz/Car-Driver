using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public Sprite muted_sound;
    public Sprite sound;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Sound") == 1 && gameObject.name == "SoundBtn")
        {
            transform.parent.transform.Find("SoundMutedBtn").gameObject.SetActive(false);
            transform.parent.transform.Find("SoundBtn").gameObject.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Sound") == 0 && gameObject.name == "SoundMutedBtn")
        {
            transform.parent.transform.Find("SoundMutedBtn").gameObject.SetActive(true);
            transform.parent.transform.Find("SoundBtn").gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
            GameObject.Find("AudioPlayer").GetComponent<AudioSource>().Play();
        if (gameObject.CompareTag("ControlButtons"))
            GetComponent<Transform>().localScale = new Vector2(70, 70);
    }
    private void OnMouseUp()
    {
        if(gameObject.CompareTag("ControlButtons"))
            GetComponent<Transform>().localScale = new Vector2(50, 50);
    }
    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "SoundBtn":
                {
                    PlayerPrefs.SetInt("Sound", 0);
                    gameObject.SetActive(false);
                    GetComponent<Transform>().localScale = new Vector2(50, 50);
                    transform.parent.transform.Find("SoundMutedBtn").gameObject.SetActive(true);
                }
                break;
            case "SoundMutedBtn":
                {
                    PlayerPrefs.SetInt("Sound", 1);
                    gameObject.SetActive(false);
                    GetComponent<Transform>().localScale = new Vector2(50, 50);
                    transform.parent.transform.Find("SoundBtn").gameObject.SetActive(true);
                }
                break;
            case "PlayBtn":
                {
                    SceneManager.LoadScene("Game");
                }
                break;
            case "RestartBtn":
                {
                    SceneManager.LoadScene("Game");
                }
                break;
            case "HomeBtn":
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
            case "QuestionBtn":
                {
                    SceneManager.LoadScene("Help");
                }
                break;
            case "ShopBtn":
                {
                    SceneManager.LoadScene("Shop");
                }
                break;
            case "LeftBtn":
                {
                    gameObject.GetComponentInParent<Shop>().MoveLeft();
                }
                break;
            case "RightBtn":
                {
                    gameObject.GetComponentInParent<Shop>().MoveRight();
                }
                break;
            case "BuyBtn":
                {
                    gameObject.GetComponentInParent<Shop>().CarSelected();
                }
                break;
        }
    }
}
