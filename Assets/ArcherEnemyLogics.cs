using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyLogics : MonoBehaviour
{
    public Transform bow;
    public Vector3 offset;

    private Transform tr;
    private Transform player;

    void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    void Update()
    {
        Vector3 diff = player.position - tr.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        bow.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}
