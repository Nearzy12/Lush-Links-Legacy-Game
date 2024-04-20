using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; //Don't forget this!

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanavas;

    private void Start()
    {
        loadMainMenu();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void loadMainMenu()
    {
        mainMenuCanavas.SetActive(true);
    }

    public void QuitGame()
    {
        print("In QuitGame function.");
        Application.Quit();
    }
}
