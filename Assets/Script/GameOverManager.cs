using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Image background;

    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI accuracyText;

    public void ShowGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        yield return FadeBackground();

        yield return new WaitForSecondsRealtime(0.5f);

        yield return FadeText();

        yield return new WaitForSecondsRealtime(0.5f);

        ResultText();
    }

    IEnumerator FadeBackground()
    {
        float alpha = 0f;

        while(alpha < 0.95f)
        {
            alpha += Time.unscaledDeltaTime;

            Color color = background.color;
            color.a = alpha;

            background.color = color;

            yield return null;
        }
    }

    IEnumerator FadeText()
    {
        float alpha = 0;

        Vector2 startPos = new Vector2(gameoverText.rectTransform.anchoredPosition.x,
            gameoverText.rectTransform.anchoredPosition.y - 100f);

        Vector2 endPos = new Vector2(gameoverText.rectTransform.anchoredPosition.x,
            gameoverText.rectTransform.anchoredPosition.y);

        gameoverText.rectTransform.anchoredPosition = startPos;

        while(alpha  < 1)
        {
            alpha += Time.unscaledDeltaTime;

            Color color = gameoverText.color;
            color.a = alpha;

            gameoverText.color = color;

            gameoverText.rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, alpha);

            yield return null;
        }
    }

    public void ResultText()
    {
        killsText.text = "Zombies Killed : " + GameStats.Instance.zombiesKilled;
        accuracyText.text = "Accuracy : " + GameStats.Instance.Accuracy.ToString("F1") + "%";
    }
}
