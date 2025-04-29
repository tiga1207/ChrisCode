using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    private float timer;
    [SerializeField] private float spawnTime =0.2f;


    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();

    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >spawnTime)
        {
            SpawnMonster();
            timer = 0f;
        }
    }

    private void SpawnMonster()
    {
        int randomType = UnityEngine.Random.Range(0,Enum.GetValues(typeof(MonsterType)).Length);
        MonsterType randomMonType = (MonsterType)randomType;
        // GameObject monster = MonsterPoolManager.instance.GetPool(MonsterType.Goblin);        
        
        
        GameObject monster = MonsterPoolManager.instance.GetPool(randomMonType);        
        monster.transform.position = spawnPoint[UnityEngine.Random.Range(1, spawnPoint.Length)].position;
    }
}
