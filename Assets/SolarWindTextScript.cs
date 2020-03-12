using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarWindTextScript : MonoBehaviour
{
    public float pulseSpeed = 2;

    private Gradient textGradient;
    private Text text;
    private bool active;
    private float gradientState;

    void Start()
    {
        gradientState = 0f;

        active = true;

        text = GetComponent<Text>();

        textGradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[2];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[4];

        colorKey[0].time = 0f;
        colorKey[0].color = Color.red;
        colorKey[1].time = 0f;
        colorKey[1].color = Color.red;

        alphaKey[0].time = 0f;
        alphaKey[0].alpha = 0f;
        alphaKey[1].time = 0.5f;
        alphaKey[1].alpha = 1f;
        alphaKey[2].time = 1f;
        alphaKey[2].alpha = 0f;

        textGradient.SetKeys(colorKey, alphaKey);
    }

    void Update()
    {
        if (active)
        {
            gradientState += Time.deltaTime * pulseSpeed;

            if (gradientState > 1f)
            {
                gradientState -= 1f;
            }

            text.color = textGradient.Evaluate(gradientState);
        }
        else
        {
            gradientState = 0f;
        }
    }

    public void SetState(bool activate)
    {
        if (activate)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }
}
