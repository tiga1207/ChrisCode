using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPatternMonster : MonsterBase
{
    private Vector3 dir;
    [SerializeField]private float timer =10f;

    protected override void Start()
    {
        base.Start();
        canTrackingPlayer = false;
    }
    public void InitDir(Vector3 direction)
    {
        Vector3 targetPos = new Vector3(direction.x, transform.position.y,direction.z);;
        transform.LookAt(targetPos);
        dir = transform.forward;
    }
    protected override void Update()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
            Debug.Log("파괴");
        }
    }
}
