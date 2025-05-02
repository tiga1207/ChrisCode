using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMove : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    public float moveSpeed = 0.05f;
    public float jumpPower = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // 캐릭터의 충돌에 의한 오브젝트 회전 방지
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Debug.Log($"입력 확인: H={h}, V={v}");

        moveDirection = new Vector3(h, 0, v).normalized;
    }

    void FixedUpdate()
    {
        Debug.Log("호출 되나요?");
        Vector3 moveOffset = moveDirection * moveSpeed;
        Debug.Log($"[속도 확인] moveSpeed: {moveSpeed}, moveOffset: {moveOffset}");
        rb.MovePosition(rb.position + moveOffset);
    }
    //  몬스터 데미지 스크립트 보고 확인할 것
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            PlayerHp playerHp = GetComponent<PlayerHp>();
            {
                if (playerHp != null)
                {
                    playerHp.TakeDamage(int damage);
                }
            }
        }
    }

    */

}
