using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    public void StartGame()
    {
        GameObject levelLoader = GameObject.FindGameObjectWithTag("Level Loader").gameObject;

        if (levelLoader != null)
        {
            levelLoader.GetComponent<LevelLoader>().GoToScene("Main Level 1");
        }
        else
        {
            Debug.Log("No Level Loader found");
        }
    }
}
