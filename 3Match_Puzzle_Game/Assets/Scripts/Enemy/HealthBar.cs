using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // 체력바로 사용할 슬라이더 UI 요소입니다.
    public Gradient gradient; // 체력에 따른 색 변화를 위한 그라디언트입니다.
    public Image fill; // 슬라이더의 Fill 이미지입니다.

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; // 슬라이더의 최대값을 설정합니다.
        slider.value = health; // 슬라이더의 현재값을 최대값으로 초기화합니다.
        fill.color = gradient.Evaluate(1f); // 최대 체력 색상으로 설정합니다.
    }

    public void SetHealth(int health)
    {
        slider.value = health; // 슬라이더의 현재값을 설정합니다.
        fill.color = gradient.Evaluate(slider.normalizedValue); // 현재 체력 비율에 따른 색상으로 설정합니다.
    }
}
