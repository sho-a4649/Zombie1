using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static bool IsShopOpen;

    public GameObject shopUI;

    public void OpenShop()
    {
        shopUI.SetActive(true);

        IsShopOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);

        IsShopOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
