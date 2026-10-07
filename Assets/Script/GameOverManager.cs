using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Image fadeImage;

    public void ShowGameOver()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float alpha = 0f;

        while(alpha < 0.9f)
        {
            alpha += Time.unscaledDeltaTime;

            Color color = fadeImage.color;
            color.a = alpha;

            fadeImage.color = color;

            yield return null;
        }
    }
}
