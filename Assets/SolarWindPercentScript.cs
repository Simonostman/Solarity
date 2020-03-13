using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SolarWindPercentScript : MonoBehaviour
{
    public GameObject warningTextObject;

    private Gradient textGradient;
    private Gradient colourGradient;
    private Text text;
    private bool active;
    private float gradientState;
    private float currentPercentage;

    void Start()
    {
        active = true;

        textGradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[2];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];

        colorKey[0].time = 0f;
        colorKey[0].color = Color.blue;
        colorKey[1].time = 1f;
        colorKey[1].color = Color.blue;

        alphaKey[0].time = 0f;
        alphaKey[0].alpha = 0f;
        alphaKey[1].time = 1f;
        alphaKey[1].alpha = 1f;

        textGradient.SetKeys(colorKey, alphaKey);

        colourGradient = new Gradient();
        colorKey = new GradientColorKey[3];
        alphaKey = new GradientAlphaKey[2];

        colorKey[0].time = 0f;
        colorKey[0].color = Color.blue;
        colorKey[1].time = 0.5f;
        colorKey[1].color = Color.green;
        colorKey[2].time = 1f;
        colorKey[2].color = Color.red;

        alphaKey[0].time = 0f;
        alphaKey[0].alpha = 1f;
        alphaKey[1].time = 1f;
        alphaKey[1].alpha = 1f;

        colourGradient.SetKeys(colorKey, alphaKey);

        text = GetComponent<Text>();

        gradientState = 0f;

        text.color = Color.blue;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "Game Complete")
        {
            gradientState = 0f;
            active = false;
        }

        if (GameObject.FindGameObjectWithTag("Earth").GetComponent<SolarWindReceptor>().DidWeWin())
        {
            active = false;
        }

        warningTextObject.GetComponent<SolarWindTextScript>().SetState(false);

        if (active)
        {
            if (gradientState < 1f)
            {
                gradientState += Time.deltaTime;

                text.color = textGradient.Evaluate(gradientState);
            }
            else
            {
                gradientState = 1f;

                currentPercentage = Mathf.Round(GameObject.FindGameObjectWithTag("Earth").GetComponent<SolarWindReceptor>().GetPercentage() * 100f);
                text.text = currentPercentage + " %";
                text.color = colourGradient.Evaluate(currentPercentage / 100f);

                if (GameObject.FindGameObjectWithTag("Earth").GetComponent<SolarWindReceptor>().GetCorrect())
                {
                    text.color = Color.yellow;
                }
                else if (currentPercentage > 100f)
                {
                    warningTextObject.GetComponent<SolarWindTextScript>().SetState(true);
                }
            }
        }
        else
        {
            if (gradientState > 0f)
            {
                gradientState -= Time.deltaTime;
            }
            else
            {
                gradientState = 0f;
            }

            text.color = textGradient.Evaluate(gradientState);
        }
    }

    public void SetState(bool state)
    {
        active = state;
    }
}
