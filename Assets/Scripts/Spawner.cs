using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private int EnemyCounter;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float SpawnRate;
    [SerializeField] private float Iterator;
    [SerializeField] private float distanceToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Iterator = SpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        Iterator -= Time.deltaTime;
        if (Iterator <= 0) 
        {
            EnemyCounter++;
            spawnPos = Player.position + 
                (Quaternion.Euler(0, 0, Random.Range(0, 360)) * 
                new Vector3(distanceToSpawn, 0, 0));
            Instantiate(Enemies[Random.Range(0, 3)], spawnPos, Quaternion.identity);
            Iterator = SpawnRate;
        }
    }
}
