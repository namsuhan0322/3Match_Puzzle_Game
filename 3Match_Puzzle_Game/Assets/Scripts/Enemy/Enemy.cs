using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityAction OnDeath; // 몬스터 사망 이벤트

    public void Die()
    {
        gameObject.SetActive(false); // 몬스터 비활성화

        if (OnDeath != null)
        {
            OnDeath.Invoke(); // 몬스터 사망 이벤트 호출
        }
    }
}