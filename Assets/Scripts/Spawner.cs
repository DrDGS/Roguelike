using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject Block;
    [SerializeField] private int EnemyCounter;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private Vector3 blockSpawnPos;
    [SerializeField] private float SpawnRate;
    [SerializeField] private float Iterator;
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToSpawnBlock;
    [SerializeField] private float minBlockScale;
    [SerializeField] private float maxBlockScale;

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
            blockSpawnPos = Player.position +
                (Quaternion.Euler(0, 0, Random.Range(0, 360)) *
                new Vector3(distanceToSpawnBlock, 0, 0));
            for (int i = 0; i < EnemyCounter; ++i)
                Instantiate(Enemies[Random.Range(0, 3)], spawnPos, Quaternion.identity);
            GameObject newBlock = Instantiate(Block, blockSpawnPos, Quaternion.identity);
            newBlock.transform.localScale = new Vector3(Random.Range(minBlockScale, maxBlockScale + 1), Random.Range(minBlockScale, maxBlockScale + 1), 1);
            Iterator = SpawnRate;
        }
    }
}
