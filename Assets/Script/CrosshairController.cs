using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public RectTransform top;
    public RectTransform bottom;
    public RectTransform left;
    public RectTransform right;

    public float baseSpread = 30f;

    public float shootSpread;
    public float jumpSpread;

    public float shootRecoverSpeed = 25f;

    void Update()
    {
        shootSpread = Mathf.MoveTowards(
            shootSpread,
            0,
            shootRecoverSpeed * Time.deltaTime
        );

        float totalSpread =
            baseSpread +
            shootSpread +
            jumpSpread;

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
}