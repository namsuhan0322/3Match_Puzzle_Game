using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 8f; //탄알 이동 속력
    public int damage = 20; // 탄알이 적에게 줄 데미지
    private Rigidbody2D rigid; //이동에 사용할 리지드바디 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        // 이동에 사용할 리지드바디 컴포넌트 가져오기
        rigid = GetComponent<Rigidbody2D>();

        // 리지드바디의 속도 = 오른쪽 방향 * 이동 속력
        rigid.velocity = -transform.right * speed;

        // 3초 뒤에 자신의 게임 오브젝트 파괴
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌한 상대방 게임 오브젝트가 Player 태그를 가진 경우
        if (collision.tag == "Player")
        {
            // 상대방 게임 오브젝트에서 PlayerHealth 컴포넌트 가져오기
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            //상대방으로부터 Enemy 컴포넌트를 가져오는 데 성공했다면
            if (playerHealth != null)
            {
                //상대방 Enemy 컴포넌트의 Die 메서드 실행
                playerHealth.TakeDamage(damage);
                
                gameObject.SetActive(false);
            }
        }
    }
}
