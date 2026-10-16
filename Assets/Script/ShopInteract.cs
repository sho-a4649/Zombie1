using UnityEngine;

public class ShopInteract : MonoBehaviour
{
    public ShopManager shopManager;

    private void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.GetComponentInParent<ShopInteract>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ShopManager.IsShopOpen)
                    {
                        shopManager.CloseShop();
                    }
                    else
                    {
                        shopManager.OpenShop();
                    }
                }
            }
        }
    }
}
