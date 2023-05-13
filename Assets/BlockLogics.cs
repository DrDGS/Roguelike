using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogics : MonoBehaviour
{
    public float TTL;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TTL -= Time.deltaTime;
        if (TTL <= 0)
            Destroy(gameObject);
    }
}
