using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject sceneDropdown;
    [SerializeField] GameObject quitButton;

    private void Awake()
    {
        DisableMainMenu();
    }

    public void ToggleMainMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        sceneDropdown.SetActive(!sceneDropdown.activeSelf);
        quitButton.SetActive(!quitButton.activeSelf);
    }

    public void DisableMainMenu()
    {
        menuPanel.SetActive(false);
        sceneDropdown.SetActive(false);
        quitButton.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
