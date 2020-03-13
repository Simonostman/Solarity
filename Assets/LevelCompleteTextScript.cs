using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteTextScript : MonoBehaviour
{
    private Gradient gradient;
    private Text text;
    private bool active;
    private float gradientState;

    void Start()
    {
        text = GetComponent<Text>();
        active = false;
        gradientState = 0f;

        gradient = new Gradient();
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        GradientColorKey[] colorKey = new GradientColorKey[2];

        alphaKey[0].time = 0f;
        alphaKey[0].alpha = 0f;
        alphaKey[1].time = 1f;
        alphaKey[1].alpha = 1f;

        colorKey[0].time = 0f;
        colorKey[0].color = Color.magenta;
        colorKey[1].time = 1f;
        colorKey[1].color = Color.magenta;

        gradient.SetKeys(colorKey, alphaKey);

        text.color = gradient.Evaluate(0f);
    }

    void Update()
    {
        if (active)
        {
            gradientState += Time.deltaTime;
            text.color = gradient.Evaluate(gradientState);
        }
        else
        {
            gradientState = 0f;
        }

        text.color = gradient.Evaluate(gradientState);

        if (GameObject.FindGameObjectWithTag("Earth").GetComponent<SolarWindReceptor>().DidWeWin())
        {
            active = true;
        }
    }

    public void SetState(bool state)
    {
        active = state;
    }
}
