using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;



public class PlayerAttack : MonoBehaviour
{
    public int attackPower = 1;
    public int attackSpeed = 1;
    public BulletSpawn bulletPerfab;

    private float attackTimer;
    public Transform attackPoint;  // 공격이 나갈 위치
    private AttackTracker tracker;


    private void Start()
    {
        tracker = GetComponent<AttackTracker>();
        attackTimer = 0f;
    }

    private void Update()
    {
        // 시간에 따른 공격 속도 설정  speed = 1 이면 1초에 1번

        attackTimer += Time.deltaTime;

        if (attackTimer >= 1f / attackSpeed)
        {
            attackTimer = 0f;
            Debug.Log("초발사");
            TryShoot();
        }
    }

    public void TryShoot()
    {

        if (tracker.nearestTarget != null && bulletPerfab != null)
        {
            Debug.Log("발사");

            // 공격이 나갈 방향
           

            Vector3 mostos = tracker.nearestTarget.position;

            mostos.y = attackPoint.position.y;
            Vector3 direction = (mostos - attackPoint.position).normalized;
            //direction.y = attackPoint.position.y;


            //  해당 방향으로의 회전값
            Quaternion rotation = Quaternion.LookRotation(direction);   

            // 소환
            BulletSpawn newBullet = BulletSpawn.Spawn(bulletPerfab, attackPoint.position , rotation);
            newBullet.BulletStartDirection(direction);
            newBullet.attackPower = attackPower;

        }
    }

    //  공격이 나갈 위치 -  무기에 추가해서 설정
    public void SetAttackPoint(Transform point)
    {
        attackPoint = point;
    }

}
