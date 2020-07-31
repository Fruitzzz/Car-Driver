using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<GameObject> cars;
    public List<int> prices;
    public GameObject car_holder;

    private string bought;
    private int cur_index;
    private void Start()
    {
        cur_index = 0;
        Sold();
        SetPrice();
        NoRoll();
    }
    private void Sold()
    {
        bought = PlayerPrefs.GetString("Cars");
        foreach (char el in bought)
        {
            prices[(Convert.ToInt32(el.ToString(), fromBase:10))] = 0;
        }
    }
    public void MoveRight()
    {
        if (cur_index != 7)
        {
            ++cur_index;
            SwapCar();
        }
    }
    public void MoveLeft()
    {
        if (cur_index != 0)
        {
            --cur_index;
            SwapCar();
        }
    }
    private void SwapCar ()
    {
        Destroy(car_holder.transform.GetChild(0).gameObject);
        car_holder.transform.DetachChildren();
        if (cur_index != 5 && cur_index != 6)
            Instantiate(cars[cur_index], car_holder.transform).transform.localScale = new Vector3(1, 1, 1);
        else
            Instantiate(cars[cur_index], car_holder.transform).transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        SetPrice();
        RenameCar();
        NoRoll();
    }
    private void SetPrice ()
    {
        if (prices[cur_index] == 0)
        {
            GameObject.Find("PriceImage").GetComponent<Image>().enabled = false;
            GameObject.Find("Price").GetComponent<Text>().text = "Куплено";
        }
        else
        {
            GameObject.Find("PriceImage").GetComponent<Image>().enabled = true;
            GameObject.Find("Price").GetComponent<Text>().text = prices[cur_index].ToString();
        }
    }
    public void CarSelected ()
    {
        if (prices[cur_index] == 0)
            PlayerPrefs.SetString("CurrentCar", car_holder.transform.GetChild(0).gameObject.name);
        else if (prices[cur_index] <= PlayerPrefs.GetInt("Balance"))
        {
            PlayerPrefs.SetString("Cars", PlayerPrefs.GetString("Cars") + cur_index.ToString());
            PlayerPrefs.SetString("CurrentCar", car_holder.transform.GetChild(0).gameObject.name);
            PlayerPrefs.SetInt("Balance", PlayerPrefs.GetInt("Balance") - prices[cur_index]);
            Sold();
            SetPrice();
        }
    }

    private void RenameCar()
    {
        string name = car_holder.transform.GetChild(0).name;
        name = name.Remove(name.IndexOf('('));
        car_holder.transform.GetChild(0).name = name;
    }
    private void NoRoll()
    {
        GameObject cur_car = car_holder.transform.GetChild(0).gameObject;
        for (int i = 1; i != cur_car.transform.childCount; ++i)
            cur_car.transform.GetChild(i).GetComponent<Animator>().enabled = false;
    }
}
