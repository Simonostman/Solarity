using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject transitionAnimationReference;
    public static LevelLoader instance;

    private GameObject transitionAnimation;
    private bool transitioning;

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
        
        transitionAnimation = Instantiate(transitionAnimationReference, transform.transform.Find("Transition Canvas"));

        transitionAnimation.SetActive(false);
    }

    private void Start()
    {
        transitioning = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            GoToScene("MainScene");
        }
    }

    public void GoToScene(string sceneType)
    {
        if (!transitioning)
        {
            StartCoroutine(SceneTransition(sceneType));

            transitioning = true;
        }
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

        transitionAnimation = Instantiate(transitionAnimationReference, transform.transform.Find("Transition Canvas"));

        transitionAnimation.SetActive(false);

        transitioning = false;

        Debug.Log("Transition Reset");
    }
}
