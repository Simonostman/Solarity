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

    private GameObject earth;

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
        earth = GameObject.FindGameObjectWithTag("Earth");
    }

    private void Start()
    {
        transitioning = false;
        zoom = Camera.main.orthographicSize;
        camPosX = Camera.main.transform.position.x;
        camPosY = Camera.main.transform.position.y;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            GoToScene("MainScene");
        }

        if (earth != null)
        {
            if (earth.GetComponent<SolarWindReceptor>().DidWeWin())
            {
                GoToScene("Next");
            }
        }
    }

    public void GoToScene(string sceneType)
    {
        ZoomCamera();
        if (!transitioning)
        {
            StartCoroutine(SceneTransition(sceneType));
            transitioning = true;
        }
    }

    private IEnumerator SceneTransition(string sceneType)
    {
        yield return new WaitForSeconds(3f);
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

    private float zoom;
    private float camPosX;
    private float camPosY;
    private void ZoomCamera()
    {
        zoom += (2.0f - zoom) * 0.01f;
        camPosX += (GameObject.Find("Earth").transform.position.x - camPosX) * 0.01f;
        camPosY += (GameObject.Find("Earth").transform.position.y - camPosY) * 0.01f;
        Camera.main.orthographicSize = zoom;
        Camera.main.transform.position = new Vector3(camPosX, camPosY, -10);
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
        earth = GameObject.FindGameObjectWithTag("Earth");
        
        zoom = Camera.main.orthographicSize;
        camPosX = Camera.main.transform.position.x;
        camPosY = Camera.main.transform.position.y;

        // Debug.Log("Transition Reset");
    }
}
