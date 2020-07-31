using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        if (transform.position.z < -16)
            Destroy(gameObject);
    }
}
