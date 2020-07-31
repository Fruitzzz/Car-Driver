using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GetComponent<Transform>().lossyScale == new Vector3(0, 0, 0))
            Destroy(gameObject);
    }
    public void Pick()
    {
        animator.SetTrigger("Picked");
    }
}
