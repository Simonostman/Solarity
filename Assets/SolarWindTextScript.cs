using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        active = false;

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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gradientState = 0f;
            active = false;
        }

        if (active || gradientState > 0.5f)
        {
            gradientState += Time.deltaTime * pulseSpeed;
        }
        else
        {
            gradientState -= Time.deltaTime * pulseSpeed;
            
            if (gradientState < 0f)
            {
                gradientState = 0f;
            }
        }

        if (gradientState > 1f)
        {
            if (active)
            {
                gradientState -= 1f;
            }
            else
            {
                gradientState = 0f;
            }
        }

        text.color = textGradient.Evaluate(gradientState);
    }

    public void SetState(bool state)
    {
        active = state;
    }
}
