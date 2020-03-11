using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Aurora
{
    //  Intensity is a value between 0 and 1
    private float intensity;

    private float hue1;
    private float hue2;
    private float hue3;

    public Aurora()
    {
        hue1 = new float();
        hue2 = new float();
        hue3 = new float();
    }

    public void AddIntensity(float addIn)
    {
        Mathf.Clamp(addIn, 0f, 1f);

        intensity += addIn;

        intensity = Mathf.Clamp(intensity, 0f, 1f);

        hue1 = intensity;
        hue2 = intensity;
        hue3 = intensity;
    }

    public Color[] GetColours()
    {
        Color[] returnCol = new Color[] { GetHue(1, hue1), GetHue(2, hue2), GetHue(3, hue3)};

        return returnCol;
    }

    public Gradient GetGradient()
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[4];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];

        Color[] colours = GetColours();

        colorKey[0].color = colours[0];
        colorKey[0].time = 0f;
        colorKey[1].color = colours[1];
        colorKey[1].time = 0.33f;
        colorKey[2].color = colours[2];
        colorKey[2].time = 0.67f;
        colorKey[3].color = new Color(1, 1, 1, 0);
        colorKey[3].time = 1f;

        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 0f;
        alphaKey[1].alpha = 1f;
        alphaKey[1].time = 1f;

        gradient.SetKeys(colorKey, alphaKey);

        return gradient;
    }

    //public Color GetColor()
    //{
    //    Color returnCol = new Color((hue1.y + hue2.y + hue3.y) / 3, (hue1.x + hue2.x + hue3.x) / 3, ((hue1.x + hue2.x + hue3.x) / 3 + (hue1.y + hue2.y + hue3.y) / 3) / 2, intensity);

    //    return returnCol;
    //}

    private Color GetHue(int hueI, float hueF)
    {
        Color returnCol;

        switch (hueI)
        {
            case 1:
                if (hueF < 0.25f)
                {
                    returnCol = new Color(hueF * 4, 0, 0, intensity * 4);
                }
                else if (hueF < 0.5f)
                {
                    returnCol = new Color(1, 0, hueF - (0.5f - hueF), 1);
                }
                else
                {
                    returnCol = new Color(1 - (0.2f) * (hueF - 0.5f), hueF * 0.625f * (hueF - 0.5f), hueF * 0.8f, 1);
                }

                return returnCol;

            case 2:
                if (hueF < 0.5f)
                {
                    returnCol = new Color(hueF /4, hueF, 0, intensity);
                }
                else
                {
                    returnCol = new Color(0.5f - (hueF / 2) * (hueF - 0.5f), hueF, 0, intensity);
                }
                returnCol = new Color(hueF / 8, hueF, 0, intensity);

                return returnCol;

            default:
                returnCol = new Color(hueF * 0.625f, hueF, hueF * 0.95f, intensity);
                return returnCol;

        }
    }
}
