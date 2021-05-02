using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerUIController : MonoBehaviour
{
    public QuitMenu quitMenu;

    private void Start()
    {
        if (quitMenu is null) Debug.LogWarning("PlayerUIController: Please set the QuitMenu in the inspector");
    }
    
    public void OpenMenuListener(CallbackContext context)
    {
        if (context.ReadValueAsButton() == true && context.performed == false)
        {
            if (quitMenu != null) quitMenu.Toggle();
        }
    }
}
