using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoadController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] types_of_ground;
    public GameObject[] cars;
    public float speed;
    public GameObject DeathScreen;

    private bool imported;
    private GameObject current_ground;
    private GameObject cur_car;
    private GameObject cur_coin;
    private Transform road;
    private void Start()
    {
        imported = false;
        road = GetComponent<Transform>();
        current_ground = Instantiate(types_of_ground[Random.Range(0, 3)], new Vector3(0, 0, 0), Quaternion.identity, road);
        current_ground.GetComponent<Movement>().speed = speed;
        cur_car = CarsCreate();
        CoinsCreate();
    }
    private void FixedUpdate()
    {
        if (current_ground.transform.position.z < 0)
            GroundController();
        if (cur_car.transform.position.z < 4)
            cur_car = CarsCreate();
        if (Mathf.RoundToInt(Time.realtimeSinceStartup) % 10 == 0 && cur_coin == null)
           CoinsCreate();
        ChangeSpeed();
    }
    private void Update()
    {
        if (player.GetComponent<Player>().IsCrashed())
            GameOver();
    }
    private void GroundController()
    {
        current_ground = Instantiate(types_of_ground[Random.Range(0, 3)], new Vector3(0, 0, current_ground.transform.position.z + 14f), Quaternion.identity, road);
        current_ground.GetComponent<Movement>().speed = speed;
    }
    private GameObject CarsCreate ()
    {
        GameObject car = null;
        List<Vector3> positions = new List<Vector3>() { new Vector3(19.51f, 0.1f, 11.11f), new Vector3(19.51f, 0.1f, 13.85f), new Vector3(19.51f, 0.1f, 16.09f), 
                                                        new Vector3(19.51f, 0.1f, 18.09f), new Vector3(18.55f, 0.1f, 11.11f), new Vector3(18.55f, 0.1f, 13.85f), 
                                                        new Vector3(18.55f, 0.1f, 16.09f), new Vector3(18.55f, 0.1f, 21.09f), new Vector3(20.51f, 0.1f, 11.11f),
                                                        new Vector3(20.51f, 0.1f, 13.85f), new Vector3(20.51f, 0.1f, 16.09f), new Vector3(20.51f, 0.1f, 18.09f),
                                                        new Vector3(21.46f, 0.1f, 11.11f), new Vector3(21.46f, 0.1f, 13.85f), new Vector3(21.46f, 0.1f, 16.09f), new Vector3(21.46f, 0.1f, 21.09f) };
        int r = Random.Range(1, 5);
        for (int i = 0; i != r; ++i)
        {
            int cur_pos = Random.Range(0, positions.Count);
            car = Instantiate(cars[Random.Range(0, 7)], positions[cur_pos], Quaternion.identity, road);
            car.transform.Rotate(new Vector3(0, 180, 0));
            positions.RemoveAt(cur_pos);
            car.AddComponent<Movement>().speed = speed * 1.5f;
            car.AddComponent<BoxCollider>();
        }
        return car;
    }
    private void CoinsCreate()
    {
        List<Vector3> positions = new List<Vector3>() { new Vector3(19.51f, 0.2f, 11.11f), new Vector3(19.51f, 0.2f, 13.85f), new Vector3(19.51f, 0.2f, 16.09f),
                                                        new Vector3(19.51f, 0.2f, 18.09f), new Vector3(18.55f, 0.2f, 11.11f), new Vector3(18.55f, 0.2f, 13.85f),
                                                        new Vector3(18.55f, 0.2f, 16.09f), new Vector3(18.55f, 0.2f, 21.09f),new Vector3(20.51f, 0.2f, 11.11f),
                                                        new Vector3(20.51f, 0.2f, 13.85f), new Vector3(20.51f, 0.2f, 16.09f), new Vector3(20.51f, 0.2f, 18.09f),
                                                        new Vector3(21.46f, 0.2f, 11.11f),new Vector3(21.46f, 0.2f, 13.85f), new Vector3(21.46f, 0.2f, 16.09f), new Vector3(21.46f, 0.2f, 21.09f) };
        cur_coin = Instantiate(Resources.Load("Objects/Coins/Coin") as GameObject, positions[Random.Range(0, positions.Count)], Quaternion.identity, road);
        cur_coin.GetComponent<Movement>().speed = speed;
    }

    private void GameOver ()
    {
        DeathScreen.SetActive(true);
        if (imported == false)
        {
            imported = true;
            player.GetComponent<Player>().ImportToPrefs();
            speed = 0;
            for (int i = 0; i != road.childCount; ++i)
                road.GetChild(i).GetComponent<Movement>().speed = 0;
        }
        GetComponent<AudioSource>().Stop();
    }
    private void ChangeSpeed ()
    {
        float score =  Time.timeSinceLevelLoad;
        if (score == 3)
            speed++;
        else if (score == 40)
            speed++;
        else if (score == 80)
            speed++;
        else if (score == 160)
            speed++;
        else if (score == 320)
            speed++;
        else if (score == 640)
            speed++;
    }
}
