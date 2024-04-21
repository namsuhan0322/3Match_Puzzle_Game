using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f; //탄알 이동 속력
    private Rigidbody2D rigid; //이동에 사용할 리지드바디 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        // 이동에 사용할 리지드바디 컴포넌트 가져오기
        rigid = GetComponent<Rigidbody2D>();

        // 리지드바디의 속도 = 오른쪽 방향 * 이동 속력
        rigid.velocity = transform.right * speed;

        // 3초 뒤에 자신의 게임 오브젝트 파괴
        Destroy(gameObject, 3f);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌한 상대방 게임 오브젝트가 Enemy 태그를 가진 경우
        if (collision.tag == "Enemy")
        {
            //상대방 게임 오브젝트에서 Enemy 컴포넌트 가져오기
            Enemy enemy = collision.GetComponent<Enemy>();

            //상대방으로부터 Enemy 컴포넌트를 가져오는 데 성공했다면
            if (enemy != null)
            {
                //상대방 Enemy 컴포넌트의 Die 메서드 실행
                enemy.Die();
            }
        }
    }
}