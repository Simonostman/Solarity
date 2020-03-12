using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SolarWindReceptor : MonoBehaviour
{
    public float windIntensity = 0.25f;
    public float marginOfError = 1;
    public float timeWindow = 1;
    public float goalIntensity = 1.5f;
    public float intensityChange = 1.0f;
    public float goalTimer = 2.0f;

    private float timeTime;
    private float currentIntensity;
    private float addIntensity;
    private float setIntensity;
    private float goalCounter;
    private bool achievedTarget;

    private Aurora aurora;
    public GameObject northernLights;
    public GameObject southernLights;

    void Start()
    {
        aurora = new Aurora();
        //northernLights = transform.Find("Northern Lights").gameObject;

        timeTime = 0f;
        achievedTarget = false;
    }

    void Update()
    {
        if (timeTime <= timeWindow)
        {
            timeTime += Time.deltaTime;
        }
        else
        {
            timeTime -= timeWindow;
            currentIntensity = addIntensity;
            addIntensity = 0;
        }

        if (currentIntensity > goalIntensity / timeWindow - marginOfError && currentIntensity < goalIntensity / timeWindow + marginOfError)
        {
            goalCounter += Time.deltaTime;

            if (goalCounter >= goalTimer)
            {
                achievedTarget = true;



                Debug.Log("Achieved target");
            }
        }
        else
        {
            goalCounter = 0;
        }

        //Debug.Log("AddIntensity: " + addIntensity + " CurrentIntensity: " + currentIntensity);

        if (setIntensity < currentIntensity)
        {
            setIntensity += intensityChange * Time.deltaTime;
        }
        else if (setIntensity > currentIntensity)
        {
            setIntensity -= intensityChange * Time.deltaTime;
        }

        aurora.SetIntensity((setIntensity / timeWindow) / 10);

        //Debug.Log("setIntensity: " + (setIntensity / timeWindow));

        northernLights.GetComponent<VisualEffect>().SetGradient("Hue", aurora.GetGradient());
        southernLights.GetComponent<VisualEffect>().SetGradient("Hue", aurora.GetGradient());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Wind")
        {
            addIntensity += windIntensity;
            Destroy(other.GetComponent<SolarWindController>().effect.gameObject);
            Destroy(other);
        }
    }

    public bool DidWeWin()
    {
        return achievedTarget;
    }
}
