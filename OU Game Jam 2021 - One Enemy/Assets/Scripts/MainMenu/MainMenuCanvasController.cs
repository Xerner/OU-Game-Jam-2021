using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject MainView;
    [SerializeField]
    private GameObject InstructionsView;
    [SerializeField]
    private SceneLoader sceneLoader;

    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Awake()
    {
        ReturnButtonPressed();
    }
    public void PlayButtonPressed()
    {
        sceneLoader.LoadNextPhase();
    }
    public void InstructionsButtonPressed()
    {
        MainView.SetActive(false);
        InstructionsView.SetActive(true);
    }
    public void ReturnButtonPressed()
    {
        MainView.SetActive(true);
        InstructionsView.SetActive(false);
    }
    public void QuitButtonPressed()
    {
        Debug.Log("I QUIT");
        Application.Quit();
    }


}
