using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogics : MonoBehaviour
{
    public float TTL;

    void Start()
    {

    }

    void Update()
    {
        TTL -= Time.deltaTime;
        if (TTL <= 0)
            Destroy(gameObject);
    }
}
