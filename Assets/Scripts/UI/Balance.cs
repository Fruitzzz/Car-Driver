using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Cars") == string.Empty)
            PlayerPrefs.SetString("Cars", "0");
    }
    private void Update()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Balance").ToString();
    }
}
