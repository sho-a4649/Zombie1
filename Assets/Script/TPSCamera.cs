using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Vector3 normalPosition = new Vector3(0.8f, 0.5f, -3.5f);

    public Vector3 aimPosition = new Vector3(0.3f, 0.5f, -2f);

    public float moveSpeed = 10f;

    private void Update()
    {
        Vector3 targetPosition = Input.GetMouseButton(1) ? aimPosition : normalPosition;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}
