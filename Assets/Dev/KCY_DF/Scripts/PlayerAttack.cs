using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Pool;

/*
 기본 공격은 레이케스트를 이용하여 플레이어 기준 가장 가까운 적에게 공격을 발사한다.
  ㄴ 360도로 레이저를 쏜다.
 공격 범위는 게임 플레이중 난이도 조절 시 수정한다.
 기본 공격의 발사 속도는 몬스터의 이동속도와 스폰되는 속도에 따라 차후 수정한다.
 기본 공격의 데미지는 몬스터의 체력에 따라 차후 수정한다. 
 많은 탄환을 오브젝트 풀로 구현한다.
 */

public class NewBehaviourScript : MonoBehaviour
{
    float AttackPower = 1;
    float AttackSpeed = 1;
    public GameObject PlayerBullet;
    private List<GameObject> PlayerBulletList = new List<GameObject>();
    public int size = 150;

    /*public void PlayerAttack(Monset monster)
    {
       
    
    }
    */

  

    // Start is called before the first frame update
    private void Awake()
    {

        for (int i = 0; i < size; i++)
        {
            GameObject instance = Instantiate(PlayerBullet);
            instance.gameObject.SetActive(false);
            PlayerBulletList.Add(instance);
        }
    }

   /* public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (PlayerBulletList.Count == 0)
        {
            return Instantiate(PlayerBullet);
        }
        GameObject instance = PlayerBulletList[PlayerBulletList - 1];
        PlayerBulletList.RemoveAt(PlayerBulletList.Count - 1);

        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);

        return instance;

    }
   */

    // Update is called once per frame
    void Update()
    {
        
    }
}
