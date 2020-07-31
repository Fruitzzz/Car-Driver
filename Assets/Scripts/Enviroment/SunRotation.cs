using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float scroll_speed;
    public Transform rotate;
    void Update()
    {
        rotate.Rotate(new Vector3(0, scroll_speed, 0));
    }
}
