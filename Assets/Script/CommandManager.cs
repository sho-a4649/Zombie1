using TMPro;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public TMP_InputField inputField;

    public static bool money;
    public static bool fullArmor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterCommand();
        }
    }

    public void EnterCommand()
    {
        string command = inputField.text.ToUpper();

        switch(command)
        {
            case "MONEY":
                money = true;
                break;

            case "FULLARMOR":
                fullArmor = true;
                break;
        }

        inputField.text = "";
    }

    public static void resetCommands()
    {
        money = false;
        fullArmor = false;
    }
}
