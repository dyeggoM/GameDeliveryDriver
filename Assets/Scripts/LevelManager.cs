using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        if (level < 0) 
            level = 0;
        if (level >= SceneManager.sceneCountInBuildSettings)
            level = SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(level);
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        LoadLevel(nextSceneIndex);
    }

    public int GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
