using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject GameOverText;
    [SerializeField] float maxTime = 120f;
    [SerializeField] GameObject DamageEffectImage;

    float timerLeft;
    Image timerBar;
    
    void Start()
    {
        GameOverText.SetActive(false);
        DamageEffectImage.SetActive(false);
        timerBar = GetComponent<Image>();
        timerLeft = maxTime;
    }

    void Update()
    {
        if (timerLeft > 0)
        {
            timerLeft -= Time.deltaTime;
            timerBar.fillAmount = timerLeft / maxTime;
            
            if (timerLeft <= 40f)
            {
                DamageEffectImage.SetActive(true);
            }
        }
        else
        {
            GameOverText.SetActive(true);
            
            DamageEffectImage.SetActive(false);
        }
    }
}