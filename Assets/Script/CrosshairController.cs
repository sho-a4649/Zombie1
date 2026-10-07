using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public RectTransform top;
    public RectTransform bottom;
    public RectTransform left;
    public RectTransform right;

    public bool isAiming;

    public float normalSpread = 30f;
    public float aimSpread = 5f;

    //public float baseSpread = 30f;

    public float shootSpread;
    public float jumpSpread;
    public float currentSpread;

    public float shootRecoverSpeed = 25f;

    private void Start()
    {
        currentSpread = normalSpread;
    }

    void Update()
    {
        shootSpread = Mathf.MoveTowards(
            shootSpread,
            0,
            shootRecoverSpeed * Time.deltaTime
        );

        float targetSpread = isAiming ? aimSpread : normalSpread;

        float spreadSpeed = isAiming ? 100f : 30f;

        currentSpread = Mathf.MoveTowards(currentSpread, targetSpread, spreadSpeed * Time.deltaTime);

        float totalSpread = VisualSpread;

        top.anchoredPosition =
            new Vector2(0, totalSpread);

        bottom.anchoredPosition =
            new Vector2(0, -totalSpread);

        left.anchoredPosition =
            new Vector2(-totalSpread, 0);

        right.anchoredPosition =
            new Vector2(totalSpread, 0);
    }

    public void AddShootSpread(float amount)
    {
        shootSpread += amount;

        shootSpread = Mathf.Clamp(
            shootSpread,
            0,
            70f
        );
    }

    public float VisualSpread
    {
        get
        {
            return currentSpread + shootSpread + jumpSpread;
        }
    }

    public float AccuracySpread
    {
        get
        {
            float spread = shootSpread /** 0.5f*/ + jumpSpread;

            if (isAiming)
                spread *= 0.2f;

            return spread;
        }
    }
}