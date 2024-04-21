using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 공격 버튼들을 담을 배열
    public Attack[] attacks;

    /*void Start()
    {
        // 모든 공격 버튼에 대해 클릭 이벤트를 추가
        foreach (Attack attack in attacks)
        {
            attack.GetComponent<Button>().onClick.AddListener(() => attack.PerformAttack());
        }
    }*/
}