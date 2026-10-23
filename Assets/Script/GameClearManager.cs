using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearManager : MonoBehaviour
{
    public GameObject clearPanel;

    public Image background;

    public TextMeshProUGUI clearText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI accuracyText;

    public GameObject titleButton;

    private bool isClearShown = false;

    public void ShowClear()
    {
        if (isClearShown)
            return;

        isClearShown = true;

        StartCoroutine(ClearSequence());

        /*Debug.Log("Kills : " + GameStats.Instance.zombiesKilled);
        Debug.Log("Accuracy : " + GameStats.Instance.Accuracy);*/
    }

    IEnumerator ClearSequence()
    {
        clearPanel.SetActive(true);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        yield return FadeBackground();

        yield return FadeText(clearText);

        yield return FadeResultText(killsText, "Zombies Killed : 0");

        yield return CountKills();

        yield return FadeResultText(accuracyText, "Accuracy : 0%");

        yield return CountAccuracy();

        titleButton.SetActive(true);
    }

    IEnumerator FadeBackground()
    {
        float alpha = 0f;

        while (alpha < 1)
        {
            alpha += Time.unscaledDeltaTime;

            Color color = background.color;
            color.a = alpha;
            background.color = color;

            yield return null;
        }
    }

    IEnumerator FadeText(TextMeshProUGUI text)
    {
        Color color = text.color;
        color.a = 0;
        text.color = color;

        float alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.unscaledDeltaTime;

            color.a = alpha;
            text.color = color;

            yield return null;
        }
    }

    IEnumerator CountKills()
    {
        int current = 0;
        int target = GameStats.Instance.zombiesKilled;

        while (current < target)
        {
            current += Mathf.Max(1, target / 50);

            if (current > target)
                current = target;

            killsText.text = "Zombies Killed : " + current;

            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator CountAccuracy()
    {
        float current = 0;
        float target = GameStats.Instance.Accuracy;

        while (current < target)
        {
            current += target / 50f;

            if (current > target)
                current = target;

            accuracyText.text = "Accuracy : " + current.ToString("F1") + "%";

            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    public void ReturnTitle()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator FadeResultText(TextMeshProUGUI text, string content)
    {
        text.text = content;

        Color color = text.color;
        color.a = 0;
        text.color = color;

        float alpha = 0;

        while(alpha < 1)
        {
            alpha += Time.unscaledDeltaTime * 2f;

            color.a = alpha;
            text.color = color;

            yield return null;
        }
    }
}
