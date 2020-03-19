using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewLevelTextScript : MonoBehaviour
{
    private Gradient gradient;
    private Text text;
    private bool active;
    private float gradientState;
    private float timer;
    private const float timerMax = 2.5f;

    void Start()
    {
        text = GetComponent<Text>();
        active = true;
        gradientState = 0f;
        timer = timerMax;

        gradient = new Gradient();
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        GradientColorKey[] colorKey = new GradientColorKey[2];

        alphaKey[0].time = 0f;
        alphaKey[0].alpha = 1f;
        alphaKey[1].time = 1f;
        alphaKey[1].alpha = 0f;

        colorKey[0].time = 0f;
        colorKey[0].color = Color.black;
        colorKey[1].time = 1f;
        colorKey[1].color = Color.black;

        gradient.SetKeys(colorKey, alphaKey);

        text.color = gradient.Evaluate(0f);
    }

    void Update()
    {
        if (active)
        {
            if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().name != "Game Complete")
            {
                text.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
            }
            else
            {
                text.text = "";
            }

            text.color = gradient.Evaluate(gradientState);
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                gradientState += Time.deltaTime;

                if (gradientState >= 1f)
                {
                    active = false;
                }
            }
        }
        else
        {
            gradientState = 1f;
        }

        text.color = gradient.Evaluate(gradientState);
    }

    public void SetState(bool state)
    {
        active = state;
        gradientState = 0f;
        timer = timerMax;
    }
}
