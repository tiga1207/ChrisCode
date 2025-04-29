using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoBehaviour
{
    public static MonsterPoolManager instance;
    [SerializeField] private GameObject[] prefabs;
    private List<GameObject>[] pools;
    [SerializeField] private int poolSize = 5;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }   
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    void Init()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i<prefabs.Length ; i++)
        {
            pools[i] = new List<GameObject>();

            for(int j = 0; j<  poolSize; j++)
            {
                GameObject monster = Instantiate(prefabs[i]);
                monster.SetActive(false);
                pools[i].Add(monster);
            }
        }
    }
    public GameObject GetPool(MonsterType type)
    {
        int index = (int)type;

        foreach(var mon in pools[index])
        {
            if(!mon.activeSelf)
            {
                mon.SetActive(true);
                return mon;
            }
        }

        GameObject newMon = Instantiate(prefabs[index]);
        pools[index].Add(newMon);
        return newMon;
    }

    public void ReturnPool(MonsterBase monster)
    {
        monster.gameObject.SetActive(false);
    }



}
