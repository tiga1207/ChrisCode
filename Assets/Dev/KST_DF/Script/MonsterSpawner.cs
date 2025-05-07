using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterSpawner : MonoBehaviour
{

    public Transform m_playerPos;

    [Header("일반 몬스터 스폰")]
    [SerializeField] private float m_spawnTime =0.2f;
    private float m_spawnTimer;

    [Header("패턴 몬스터 스폰")]

    [SerializeField] private float m_patternTime = 5f;
    public GameObject[] m_straightMonsterPrefabs;
    private float m_patternCheckTimer;
    
    [Header("공통")]
    //스폰 범위 설정
    [SerializeField] private float m_spawnRange =10f;

    void Update()
    {
        NormalSpawnTimer();
        PatternSpawnTimer();
    }
    

    private void NormalSpawnTimer()
    {
        m_spawnTimer += Time.deltaTime;

        if (m_spawnTimer > m_spawnTime)
        {
            SpawnMonster();
            m_spawnTimer = 0f;
        }
    }

    private void SpawnMonster()
    {
        Vector3 spawnPos = m_playerPos.position + UnityEngine.Random.insideUnitSphere * m_spawnRange;
        spawnPos.y = 0;

        GameObject monster = MonsterPoolManager.Instance.GetRandomPool();
        monster.transform.position = spawnPos;
    }

    public void SetDieAciton(UnityAction uAciton)
    {
        m_dieAction = uAciton;
    }

    private void PatternSpawnTimer()
    {
        m_patternCheckTimer += Time.deltaTime;
        if (m_patternCheckTimer >= m_patternTime)
        {
            StraightPattern();
            m_patternCheckTimer = 0f;
        }
    }

    private void StraightPattern()
    {
        if(m_playerPos == null) return;

        for(int i = 0; i < m_straightMonsterPrefabs.Length;i++)
        {
            //플레이어 기준 랜덤 원형 위치에 몬스터 생성
            Vector3 ranPos = m_playerPos.position + UnityEngine.Random.insideUnitSphere * m_spawnRange;
            ranPos.y = 0;
            
            int prefabIndex = UnityEngine.Random.Range(0, m_straightMonsterPrefabs.Length);
            GameObject monster = Instantiate(m_straightMonsterPrefabs[prefabIndex], ranPos, Quaternion.identity);

            monster.GetComponent<StraightPatternMonster>().InitDir(m_playerPos.position);
        }
    }

    
}
