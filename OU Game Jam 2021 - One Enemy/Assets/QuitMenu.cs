using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    [SerializeField] SceneLoader sceneloader;

    private void Start()
    {
        if (sceneloader is null) Debug.LogWarning("QuitMenu: Need to initialize SceneLoader in the inspector");
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            //Cursor.lockState = CursorLockMode.;
            Time.timeScale = 1;
        }
    }

    public void Yes()
    {
        Toggle();
        sceneloader.LoadMainMenu();
    }

    public void No() 
    {
        Toggle();
    }
}
