using UnityEngine;

public class ShopPrompt : MonoBehaviour
{
    public GameObject prompt;

    private void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            prompt.SetActive(hit.collider.CompareTag("Shop"));
        }
        else
        {
            prompt.SetActive(false);
        }
    }
}
