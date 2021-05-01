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
    private GameObject SettingsView;
    [SerializeField]
    private SceneLoader sceneLoader;

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
        SettingsView.SetActive(false);
    }
    public void SettingsButtonPressed()
    {
        MainView.SetActive(false);
        InstructionsView.SetActive(false);
        SettingsView.SetActive(true);
    }
    public void ReturnButtonPressed()
    {
        MainView.SetActive(true);
        InstructionsView.SetActive(false);
        SettingsView.SetActive(false);
    }
    public void QuitButtonPressed()
    {
        Debug.Log("I QUIT");
        Application.Quit();
    }


}
