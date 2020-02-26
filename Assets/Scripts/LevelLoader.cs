﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject transitionAnimationReference;
    public static LevelLoader instance;

    private GameObject transitionAnimation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        transitionAnimation = Instantiate(transitionAnimationReference, transform.transform.Find("Transition Canvas").Find("Transition"));

        transitionAnimation.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            GoToScene("Next");
        }
    }

    public void GoToScene(string sceneType)
    {
        StartCoroutine(SceneTransition(sceneType));
    }

    private IEnumerator SceneTransition(string sceneType)
    {
        PlayTransition();

        yield return new WaitForSeconds(2f);

        switch (sceneType)
        {
            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;

            case "Menu":
                SceneManager.LoadScene("SimonMenuTest");
                break;

            default:
                SceneManager.LoadScene(sceneType);
                break;
        }
    }

    private void PlayTransition()
    {
        transitionAnimation.SetActive(true);

        StartCoroutine(EndTransition());
    }

    private IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(4f);

        Destroy(transitionAnimation);

        transitionAnimation = Instantiate(transitionAnimationReference, transform.transform.Find("Transition Canvas").Find("Transition"));

        transitionAnimation.SetActive(false);
    }
}
