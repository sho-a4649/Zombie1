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
    public GameObject titleButton;

    private void Start()
    {
        titleButton.SetActive(false);
    }

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

        yield return FadeResultText(killsText, "Zombies Killed : 0"); // + GameStats.Instance.zombiesKilled);

        yield return CountKills();

        yield return new WaitForSecondsRealtime(0.2f);

        yield return FadeResultText(accuracyText, "Accuracy : 0%"); //+ GameStats.Instance.Accuracy.ToString("F1") + "%");

        yield return CountAccuracy();

        titleButton.SetActive(true);
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

    /*public void ResultText()
    {
        killsText.text = "Zombies Killed : " + GameStats.Instance.zombiesKilled;
        accuracyText.text = "Accuracy : " + GameStats.Instance.Accuracy.ToString("F1") + "%";
    }*/

    IEnumerator FadeResultText(TextMeshProUGUI text, string content)
    {
        text.text = content;

        Color color = text.color;
        color.a = 0;
        text.color = color;

        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.unscaledDeltaTime * 2f;

            color.a = alpha;
            text.color = color;

            yield return null;
        }
    }

    IEnumerator CountKills()
    {
        int target = GameStats.Instance.zombiesKilled;

        int current = 0;

        while (current < target)
        {
            current += Mathf.Max(1, target / 50);

            if (current > target)
                current = target;

            killsText.text = "Zombies Killed : " + current;

            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    IEnumerator CountAccuracy()
    {
        float target = GameStats.Instance.Accuracy;

        float current = 0;

        while (current < target)
        {
            current += target / 50f;

            if (current > target)
                current = target;

            accuracyText.text = "Accuracy :" + current.ToString("F1") + "%";

            yield return new WaitForSecondsRealtime(0.03f);
        }
    }
}
