using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Text text_score;
    public Text text_balance;

    private int balance;
    private int score;
    private Transform position;
    private bool is_crashed;
    private int i;
    void Start()
    {
        SetCar();
        GetComponentInChildren<Transform>().GetChild(0).tag = "PlayersCar";
        is_crashed = false;
        i = 1;
        position = GetComponent<Transform>();
    }
    void Update()
    {
        text_balance.text = balance.ToString();
        if (!IsCrashed())
        {
            score = Mathf.RoundToInt(Time.timeSinceLevelLoad);
            text_score.text = score.ToString();
        }
        Movement();
    }
    private void Movement()
    {
        if (Input.touchCount > 0 && !is_crashed)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && i < 1 && touch.position.x > Screen.width / 2)
            {
                MoveRight();
            }
            if (touch.phase == TouchPhase.Began && i >= -1 && touch.position.x < Screen.width / 2)
            {
                MoveLeft();
            }
        }
    }
    public void MoveRight()
    {
        position.Translate(new Vector3(0.987f, 0, 0));
        ++i;
    }
    public void MoveLeft()
    {
        position.Translate(new Vector3(-0.987f, 0, 0));
        --i;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Car"))
        {
            is_crashed = true;
            GetComponentInChildren<Rigidbody>().isKinematic = false;
            GetComponentInChildren<Rigidbody>().useGravity = true;
            collider.GetComponent<Rigidbody>().isKinematic = false;
            collider.GetComponent<Rigidbody>().useGravity = true;
        }
        if (collider.gameObject.CompareTag("Coin"))
        {
            collider.gameObject.GetComponent<Coin>().Pick();
            balance++;
        }
    }
    public bool IsCrashed ()
    {
        return is_crashed;
    }
    public void ImportToPrefs ()
    {
        PlayerPrefs.SetInt("Balance", PlayerPrefs.GetInt("Balance") + balance);
        if (PlayerPrefs.GetInt("Score") < score)
            PlayerPrefs.SetInt("Score", score);
    }
    public void SetCar()
    {
        if (PlayerPrefs.GetString("CurrentCar") == string.Empty)
            PlayerPrefs.SetString("CurrentCar", "Car1");
        Instantiate(Resources.Load("Objects/Cars/" + PlayerPrefs.GetString("CurrentCar")), gameObject.transform);
    }
    public int GetBalance()
    {
        return balance;
    }
}
