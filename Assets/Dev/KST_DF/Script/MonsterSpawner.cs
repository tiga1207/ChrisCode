using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public Transform playerPos;
    // public Transform[] spawnPoint;

    [Header("일반 몬스터 스폰")]
    [SerializeField] private float spawnTime =0.2f;
    private float timer;

    [Header("패턴 몬스터 스폰")]

    [SerializeField] private float patternTime = 5f;
    public GameObject[] straightMonsterPrefabs;
    private float patternCheckTimer;
    
    [Header("공통")]
    //스폰 범위 설정
    [SerializeField] private float spawnRange =10f;


    // void Awake()
    // {
    //     // spawnPoint = GetComponentsInChildren<Transform>();

    // }
    void Update()
    {
        NormalSpawnTimer();
        PatternSpawnTimer();
    }
    

    private void NormalSpawnTimer()
    {
        timer += Time.deltaTime;

        if (timer > spawnTime)
        {
            SpawnMonster();
            timer = 0f;
        }
    }

    private void SpawnMonster()
    {
        int randomType = UnityEngine.Random.Range(0,Enum.GetValues(typeof(MonsterType)).Length -1);
        MonsterType randomMonType = (MonsterType)randomType;

        Vector3 ranPos = playerPos.position + UnityEngine.Random.insideUnitSphere * spawnRange;
        ranPos.y = 0;
        
        GameObject monster = MonsterPoolManager.instance.GetPool(randomMonType);        
        // monster.transform.position = spawnPoint[UnityEngine.Random.Range(1, spawnPoint.Length)].position;
        monster.transform.position = ranPos;
    }

    private void PatternSpawnTimer()
    {
        patternCheckTimer += Time.deltaTime;
        if (patternCheckTimer >= patternTime)
        {
            StraightPattern();
            patternCheckTimer = 0f;
        }
    }

    private void StraightPattern()
    {
        if(playerPos != null)
        {
            for(int i = 0; i < straightMonsterPrefabs.Length;i++)
            {
                //플레이어 기준 랜덤 원형 위치에 몬스터 생성
                Vector3 ranPos = playerPos.position + UnityEngine.Random.insideUnitSphere * spawnRange;
                ranPos.y = 0;
                
                int prefabIndex = UnityEngine.Random.Range(0, straightMonsterPrefabs.Length);
                GameObject monster = Instantiate(straightMonsterPrefabs[prefabIndex], ranPos, Quaternion.identity);

                monster.GetComponent<StraightPatternMonster>().InitDir(playerPos.position);
            }
        }
    }

    
}
