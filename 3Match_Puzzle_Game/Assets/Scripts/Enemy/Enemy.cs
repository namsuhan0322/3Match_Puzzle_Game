using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Die()
    {
        // 사망 애니메이션 트리거 설정
        _animator.SetTrigger("doDead");
        // 코루틴 시작
        StartCoroutine(DeadRoutine());
    }

    private IEnumerator DeadRoutine()
    {
        // 사망 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        // 게임 오브젝트 비활성화
        gameObject.SetActive(false);
        // GameManager를 통해 몬스터가 죽었음을 알림
        GameManager.Instance.EnemyKilled();
    }
}