using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneDropdownHandler : MonoBehaviour
{
    private Dictionary<string, int> levelDictionary = new Dictionary<string, int>();
    private LevelManager _levelManager;
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        var dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            var levelName = $"Level {i}";
            levelDictionary.Add(levelName,i);
            dropdown.options.Add(new TMP_Dropdown.OptionData()
            {
                text = levelName
            });
        }
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown);});
        dropdown.value = _levelManager.GetCurrentLevel();
        dropdown.RefreshShownValue();
    }

    private void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        dropdown.RefreshShownValue();
        // var mainMenu = FindObjectOfType<MainMenu>();
        // mainMenu.DisableMainMenu();
        var index = dropdown.value;
        var selectedName = dropdown.options[index].text;
        var selectedLevel = levelDictionary[selectedName];
        if(selectedLevel != _levelManager.GetCurrentLevel())
            _levelManager.LoadLevel(selectedLevel);
    }
}
