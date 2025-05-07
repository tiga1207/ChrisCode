using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    private ArrowPool arrowPool;
    private int damage;
   //타이머 변수
   private float timer;
   private bool isReturned = false;

    //화살 위치
   private Transform shotingTransform;
   
   [SerializeField] private float disappearArrowTime =3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //화살끼리의 충돌 무시
        Physics.IgnoreLayerCollision(this.gameObject.layer,this.gameObject.layer,true);
        //화살과 몬스터의 충돌 무시
        Physics.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Monster"),true);
    }


    void Update()
    {
        //화살 앞 방향 세팅
        if(rb.velocity.magnitude >2)
        {
            transform.forward =rb.velocity;
        }
        //타이머 동작
        timer += Time.deltaTime;
        //타이머가 화살이 사라지는 시간과 같다면 ->화살 파괴
        if(timer >= disappearArrowTime)
        {
            //풀에 반납하기
            ReturnToPool();
        }
        
    }

    //몬스터와 충돌 시
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            PlayerHp player = collision.gameObject.GetComponent<PlayerHp>();
            if(player !=null)
            {
                player.TakeDamage(damage);
                Debug.Log($"플레이어 피해 로직 호출. 데미지 : {damage}");
                
            }
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if(isReturned == true) return;
        isReturned = true;
        rb.velocity = Vector3.zero;
        arrowPool.ReturnPool(this.gameObject);
    }

    
    public void ArrowInit(Transform arrowTransform, Vector3 direction, ArrowPool pool, float arrowSpeed, int damage)
    {
        //풀링 초기화
        this.arrowPool =pool;
        //화살 데미지
        this.damage = damage;
        this.timer = 0f;
        this.isReturned = false;
        

        //화살 위치
        shotingTransform = arrowTransform;
        transform.position = arrowTransform.position;

        transform.forward = direction;


        Rigidbody bulletRb = GetComponent<Rigidbody>();
        
        //화살 속도
        // bulletRb.velocity = arrowSpeed * shotingTransform.forward;
        bulletRb.velocity = arrowSpeed * direction;

        //타이머 초기화
        timer =0f;
        //게임 오브젝트 활성화
        gameObject.SetActive(true);
    }

}
