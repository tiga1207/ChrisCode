using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitDataBase : MonoBehaviour
{
    public static TraitDataBase Instance;

    public List<Trait> allTraits = new List<Trait>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Trait> GetRandomTraits(int count)
    {
        List<Trait> randomTraits = new List<Trait>();
        List<Trait> copy = new List<Trait>(allTraits);

        for (int i = 0; i < count; i++)
        {
            if (copy.Count == 0)// 뽑을게 없으면 정지
            {
                break;
            }

            int randomIndex = Random.Range(0, copy.Count); // 랜덤으로 하나 선택
            randomTraits.Add(copy[randomIndex]); // 선택한 걸 결과 리스트에 추가
            copy.RemoveAt(randomIndex); // 이미 뽑은 건 삭제해서 중복 방지
        }

        return randomTraits; // 결과 리턴
    }
}
