using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public int zombiesKilled;

    public int shotsFired;

    public int shotsHit;

    private void Awake()
    {
        Instance = this;
    }

    public float Accuracy
    {
        get
        {
            if (shotsFired == 0)
                return 0;

            return (float)shotsHit / shotsFired * 100f;
        }
    }
}
