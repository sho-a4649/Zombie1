using UnityEngine;

public class WeaponHolderFollow : MonoBehaviour
{
    public Transform player;
    public Transform cameraPivot;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(cameraPivot.eulerAngles.x, player.eulerAngles.y, 0f);
    }
}
