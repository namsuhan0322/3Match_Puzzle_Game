using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] 
    [Range(0.01f, 10f)] 
    private float fadeTime;

    [SerializeField] 
    private AnimationCurve fadeCurve;
    private Image image;
    
    void Awake()
    {
        image = GetComponent<Image>();

        StartCoroutine(Fade(1f, 0f)); 
    }
    
    public IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            float percent = currentTime / fadeTime;
            
            Color color = image.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;
            
            yield return null;
        }
        
        Color finalColor = image.color;
        finalColor.a = end;
        image.color = finalColor;
    }
}